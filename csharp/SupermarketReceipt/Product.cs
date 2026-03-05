using System.Collections.Generic;
using Value;

namespace SupermarketReceipt;

public class Product(string name, ProductUnit unit) : ValueType<Product>
{
    public string Name { get; } = name;
    public ProductUnit Unit { get; } = unit;

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return [Name, Unit];
    }
}