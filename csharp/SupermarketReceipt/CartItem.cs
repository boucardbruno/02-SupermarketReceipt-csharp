using System.Collections.Generic;
using Value;

namespace SupermarketReceipt;

public class CartItem(Product product, double quantity) : ValueType<CartItem>
{
    public Product Product { get; } = product;
    public double Quantity { get; } = quantity;

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return [Product, Quantity];
    }
}