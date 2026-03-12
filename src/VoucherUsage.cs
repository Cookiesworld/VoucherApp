namespace VoucherApp;

public class VoucherUsage
{
    // Primary key for EF Core
    public int Id { get; private set; }

    // Foreign key to Voucher
    public int VoucherId { get; private set; }

    // Navigation property
    public Voucher Voucher { get; set; }

    public DateTime UsageDate { get; private set; }

    // Parameterless constructor required by EF Core
    public VoucherUsage() { }

    public VoucherUsage(int voucherId, DateTime usageDate)
    {
        VoucherId = voucherId;
        UsageDate = usageDate;
    }
}