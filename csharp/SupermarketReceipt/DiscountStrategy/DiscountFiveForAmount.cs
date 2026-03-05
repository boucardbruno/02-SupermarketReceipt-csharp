namespace SupermarketReceipt.DiscountStrategy;

public class DiscountFiveForAmount : IProvideDiscount
{
    public Discount Compute(Product product, Offer offer, double quantity, double unitPrice)
    {
        var quantityAsInt = (int)quantity;
        var discountTotal = unitPrice * quantity -
                            (offer.Argument * (quantity / 5) + quantityAsInt % 5 * unitPrice);
        return new Discount(product, 5 + " for " + offer.Argument.ToString("N2", ShoppingCart.Culture), -discountTotal);
    }
}