namespace VoucherApp;

public interface IVoucherRepository
{
    public Voucher? GetByCode(string code);
    public IEnumerable<Voucher> GetAll();
    public bool IsUsed(string code);
    public void AddVoucher(Voucher voucher);
    public bool ValidateVoucher(Voucher voucher);
    public void MarkVoucherAsUsed(Voucher voucher);
}