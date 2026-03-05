namespace SupermarketReceipt.DiscountStrategy;

public class DiscountTwoForAmount : IProvideDiscount
{
    public Discount Compute(Product product, Offer offer, double quantity, double unitPrice)
    {
        var quantityAsInt = (int)quantity;
        var total = offer.Argument * (quantity / 2) + quantityAsInt % 2 * unitPrice;
        var discountN = unitPrice * quantity - total;
        return new Discount(product, "2 for " + offer.Argument.ToString("N2", ShoppingCart.Culture), -discountN);
    }
}