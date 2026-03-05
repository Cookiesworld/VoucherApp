namespace VoucherApp;

public interface IVoucherRepository
{
    public Voucher? GetByCode(string code);
    public bool ValidateVoucher(Voucher voucher);
    public void MarkVoucherAsUsed(Voucher voucher);
}