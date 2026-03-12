namespace VoucherApp;

public interface IOrderProcessor
{
    public Order PlaceOrder(Policy policy, string? voucherCode);
}