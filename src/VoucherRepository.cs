namespace VoucherApp;

public class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _dbContext;

    public VoucherRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Voucher? GetByCode(string code)
    {
        return _dbContext.Vouchers.FirstOrDefault(v => v.Code == code);
    }

    /// <summary>
    /// Checks if the voucher is valid for use.
    /// </summary>
    /// <param name="voucher">The voucher to validate.</param>
    /// <returns>True if the voucher is valid, false otherwise.</returns>
    public bool ValidateVoucher(Voucher voucher)
    {
        // A valid voucher is one that has not expired, has a positive discount value, and has not been used.
        if (voucher.Expiry < DateTime.UtcNow) { return false; }

        // check if the voucher has been used        
        if (_dbContext.UsedVouchers.Any(vu => vu.Voucher.Code == voucher.Code))
        {
            return false;
        }

        if (voucher.DiscountValue <= 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Marks a voucher as used in the database.
    /// </summary>
    /// <param name="voucher">The voucher to mark as used.</param>
    public void MarkVoucherAsUsed(Voucher voucher)
    {
        _dbContext.UsedVouchers.Add(new VoucherUsage(voucher, DateTime.UtcNow));
        _dbContext.SaveChanges();
    }
}
