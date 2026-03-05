namespace VoucherApp;

public class Order
{
    public Guid Id { get; private set; }
    public string PolicyNumber { get; private set; }
    public string? VoucherCode { get; private set; }
    public decimal? DiscountAmount { get; private set; }
    public decimal? Total { get; private set; }

    public Order(Guid id, string policyNumber, decimal? total, string? voucherCode = null, decimal? discountAmount = null)
    {
        Id = id;
        PolicyNumber = policyNumber;
        VoucherCode = voucherCode;
        DiscountAmount = discountAmount;
        Total = total;
    }
}
