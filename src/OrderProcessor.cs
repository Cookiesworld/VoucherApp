namespace VoucherApp;

public class OrderProcessor : IOrderProcessor
{
    private readonly IVoucherRepository _voucherRepository;

    public OrderProcessor(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public Order PlaceOrder(Policy policy, string? voucherCode)
    {
        decimal finalPrice = policy.BasePrice;
        decimal discountAmount = 0m;
        Voucher? voucher = null;

        if (!string.IsNullOrWhiteSpace(voucherCode))
        {
            voucher = _voucherRepository.GetByCode(voucherCode);
        }

        if (voucher is not null && _voucherRepository.ValidateVoucher(voucher))
        {
            switch (voucher.DiscountType)
            {
                case DiscountType.Percentage:
                    discountAmount = policy.BasePrice * (voucher.DiscountValue / 100);
                    finalPrice -= discountAmount;
                    break;
                case DiscountType.FixedAmount:
                    discountAmount = voucher.DiscountValue;
                    finalPrice -= discountAmount;
                    break;
            }

            // Ensure the price doesn't go below zero
            if (finalPrice < 0)
            {
                finalPrice = 0;
            }

            _voucherRepository.MarkVoucherAsUsed(voucher);
            return new Order(Guid.NewGuid(), policy.PolicyNumber, finalPrice, voucher.Code, discountAmount);
        }


        // No voucher or invalid voucher, create order with base price.
        return new Order(Guid.NewGuid(), policy.PolicyNumber, finalPrice);
    }
}