namespace SupermarketReceipt.DiscountStrategy;

public class DiscountTreeForTwo : IProvideDiscount
{
    public Discount Compute(Product product, Offer _, double quantity, double unitPrice)
    {
        var quantityAsInt = (int)quantity;
        var discountAmount = quantity * unitPrice -
                             (quantity / 3 * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
        return new Discount(product, "3 for 2", -discountAmount);
    }
}