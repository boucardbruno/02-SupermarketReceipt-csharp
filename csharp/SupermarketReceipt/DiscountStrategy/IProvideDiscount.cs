namespace SupermarketReceipt.DiscountStrategy;

public interface IProvideDiscount
{
    Discount Compute(Product product, Offer offer, double quantity, double unitPrice);
}