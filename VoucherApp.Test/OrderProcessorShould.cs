using AutoFixture;
using Moq;

namespace VoucherApp.Test;

public class OrderProcessorShould
{
    [Fact]
    public void PlaceOrderWithoutVoucherReturnsOrder()
    {
        var fixture = new Fixture();
        var policy = fixture.Create<Policy>();
        var voucherRepository = new Mock<IVoucherRepository>();

        var orderProcessor = new OrderProcessor(voucherRepository.Object);
        var order = orderProcessor.PlaceOrder(policy, null);

        Assert.NotNull(order);
    }

    [Fact]
    public void PlaceOrder_WithValidVoucher_AppliesDiscount()
    {
        var fixture = new Fixture();
        var policy = fixture.Create<Policy>();
        var voucher = new Voucher("ABC", DiscountType.Percentage, 10m, DateTime.UtcNow.AddDays(1));

        var mockRepo = fixture.Freeze<Mock<IVoucherRepository>>();
        mockRepo.Setup(r => r.GetByCode("ABC")).Returns(voucher);
        mockRepo.Setup(r => r.ValidateVoucher(voucher)).Returns(true);

        var sut = new OrderProcessor(mockRepo.Object);

        var order = sut.PlaceOrder(policy, "ABC");

        Assert.Equal(policy.BasePrice * 0.9m, order.Total);
        mockRepo.Verify(r => r.MarkVoucherAsUsed(voucher), Times.Once);
    }
}
