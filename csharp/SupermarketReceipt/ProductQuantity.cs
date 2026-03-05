using System.Collections.Generic;
using Value;

namespace SupermarketReceipt;

public class ProductQuantity(Product product, double quantity) : ValueType<ProductQuantity>
{
    public Product Product { get; } = product;
    public double Quantity { get; } = quantity;

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return [Product, Quantity];
    }
}