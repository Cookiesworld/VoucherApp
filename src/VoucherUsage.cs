namespace VoucherApp;

public class VoucherUsage
{
    public VoucherUsage(Voucher voucher, DateTime usageDate)
    {
        Voucher = voucher;
        UsageDate = usageDate;
    }
    public Voucher Voucher { get; private set; }
    public DateTime UsageDate { get; private set; }
}