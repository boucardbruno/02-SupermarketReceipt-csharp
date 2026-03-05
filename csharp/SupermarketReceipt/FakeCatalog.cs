using System.Collections.Generic;

namespace SupermarketReceipt;

public class FakeCatalog : ISupermarketCatalog
{
    private readonly IDictionary<Product, double> _prices = new Dictionary<Product, double>();

    public void AddProduct(Product product, double price)
    {
        _prices.Add(product, price);
    }

    public double GetUnitPrice(Product product)
    {
        return _prices[product];
    }
}