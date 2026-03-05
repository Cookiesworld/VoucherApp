using System;

namespace VoucherApp;

public class Voucher
{
    public int Id { get; private set; } // Public for EF Core Primary Key convention
    public string Code { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public decimal DiscountValue { get; private set; }
    public DateTime Expiry { get; private set; }
    public bool IsUsed { get; private set; }

    // Parameterless constructor for EF Core
    public Voucher() { }

    public Voucher(string code, DiscountType discountType, decimal discountValue, DateTime expiry)
    {
        Code = code;
        DiscountType = discountType;
        DiscountValue = discountValue;
        Expiry = expiry;
        IsUsed = false;
    }
}
