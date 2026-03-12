using Microsoft.EntityFrameworkCore;
using VoucherApp;

Console.WriteLine("=== VoucherApp Order Demo ===\n");

var dbPath = Path.Combine(AppContext.BaseDirectory, "vouchers.db");
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite($"Data Source={dbPath}")
    .Options;

using var context = new AppDbContext(options);
context.Database.EnsureCreated();

var voucherRepo = new VoucherRepository(context);
SeedDefaultVouchers(voucherRepo);
var orderProcessor = new OrderProcessor(voucherRepo);

while (true)
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("  1) Create an order");
    Console.WriteLine("  2) Add a voucher");
    Console.WriteLine("  3) List vouchers");
    Console.WriteLine("  4) Quit");
    Console.Write("Selection: ");

    var choice = Console.ReadLine()?.Trim();
    Console.WriteLine();

    if (string.Equals(choice, "4", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(choice, "q", StringComparison.OrdinalIgnoreCase) ||
        string.Equals(choice, "quit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (string.Equals(choice, "2", StringComparison.OrdinalIgnoreCase))
    {
        AddVoucher(voucherRepo);
        continue;
    }

    if (string.Equals(choice, "3", StringComparison.OrdinalIgnoreCase))
    {
        ListVouchers(voucherRepo);
        continue;
    }

    if (!string.Equals(choice, "1", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Unknown selection. Please choose 1, 2, 3, or 4.\n");
        continue;
    }

    CreateOrder(orderProcessor);
}

Console.WriteLine("Goodbye!");

static void SeedDefaultVouchers(VoucherRepository repo)
{
    var defaults = new[]
    {
        new Voucher("PROMO10", DiscountType.Percentage, 10m, DateTime.UtcNow.AddDays(30)),
        new Voucher("TAKE5", DiscountType.FixedAmount, 5m, DateTime.UtcNow.AddDays(30)),
    };

    foreach (var v in defaults)
    {
        if (repo.GetByCode(v.Code) is null)
            repo.AddVoucher(v);
    }
}

static void CreateOrder(OrderProcessor orderProcessor)
{
    Console.Write("Enter policy number: ");
    var policyNumber = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(policyNumber))
    {
        Console.WriteLine("Policy number is required.\n");
        return;
    }

    Console.Write("Enter base price: ");
    if (!decimal.TryParse(Console.ReadLine(), out var basePrice) || basePrice < 0)
    {
        Console.WriteLine("Invalid price. Please enter a positive number.\n");
        return;
    }

    Console.Write("Enter voucher code (optional): ");
    var voucherCode = Console.ReadLine()?.Trim();

    var policy = new Policy(policyNumber, basePrice);
    var order = orderProcessor.PlaceOrder(policy, string.IsNullOrWhiteSpace(voucherCode) ? null : voucherCode);

    Console.WriteLine("\n--- Order Created ---");
    Console.WriteLine($"Order ID: {order.Id}");
    Console.WriteLine($"Policy: {order.PolicyNumber}");
    Console.WriteLine($"Base Price: {basePrice:C}");
    Console.WriteLine($"Voucher: {order.VoucherCode ?? "(none)"}");
    Console.WriteLine($"Discount: {order.DiscountAmount?.ToString("C") ?? "(none)"}");
    Console.WriteLine($"Total: {order.Total:C}\n");
}

static void AddVoucher(VoucherRepository repo)
{
    Console.Write("Enter voucher code: ");
    var code = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(code))
    {
        Console.WriteLine("Voucher code is required.\n");
        return;
    }

    Console.WriteLine("Select discount type:");
    Console.WriteLine("  1) Percentage");
    Console.WriteLine("  2) Fixed amount");
    Console.Write("Choice: ");
    var typeChoice = Console.ReadLine()?.Trim();

    var discountType = typeChoice switch
    {
        "2" => DiscountType.FixedAmount,
        _ => DiscountType.Percentage,
    };

    Console.Write("Enter discount value: ");
    if (!decimal.TryParse(Console.ReadLine(), out var discountValue) || discountValue <= 0)
    {
        Console.WriteLine("Invalid discount value.\n");
        return;
    }

    Console.Write("Valid for how many days (from today)? ");
    if (!int.TryParse(Console.ReadLine(), out var days) || days <= 0)
    {
        Console.WriteLine("Invalid number of days.\n");
        return;
    }

    var voucher = new Voucher(code, discountType, discountValue, DateTime.UtcNow.AddDays(days));
    repo.AddVoucher(voucher);

    Console.WriteLine($"Voucher '{code}' added and valid for {days} days.\n");
}

static void ListVouchers(VoucherRepository repo)
{
    var vouchers = repo.GetAll();

    if (!vouchers.Any())
    {
        Console.WriteLine("No vouchers defined.\n");
        return;
    }

    Console.WriteLine("Available vouchers:");
    foreach (var v in vouchers)
    {
        var used = repo.IsUsed(v.Code) ? "(USED)" : string.Empty;
        Console.WriteLine($" - {v.Code} [{v.DiscountType} {v.DiscountValue}] expires {v.Expiry:yyyy-MM-dd} {used}");
    }

    Console.WriteLine();
}
