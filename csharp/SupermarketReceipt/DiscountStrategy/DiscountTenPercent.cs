namespace SupermarketReceipt.DiscountStrategy;

public class DiscountTenPercent : IProvideDiscount
{
    public Discount Compute(Product product, Offer offer, double quantity, double unitPrice)
    {
        return new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
    }
}