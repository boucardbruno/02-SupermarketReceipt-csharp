using System.Collections.Generic;
using Value;

namespace SupermarketReceipt;

public class Discount(Product product, string description, double discountAmount) : ValueType<Discount>
{
    public Product Product { get; } = product;
    public string Description { get; } = description;
    public double DiscountAmount { get; } = discountAmount;

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return [Product, Description, DiscountAmount];
    }
}