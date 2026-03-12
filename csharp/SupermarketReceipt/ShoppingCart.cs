using System.Collections.Generic;
using System.Globalization;
using SupermarketReceipt.DiscountStrategy;

namespace SupermarketReceipt;

public class ShoppingCart
{
    public static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");
    private readonly StrategyDiscountFactory _discountFactory = new();
    private readonly List<CartItem> _items = [];
    private readonly Dictionary<Product, double> _productQuantities = new();

    public List<CartItem> GetItems()
    {
        return [.._items];
    }

    public void AddItemQuantity(Product product, double quantity)
    {
        _items.Add(new CartItem(product, quantity));
        _productQuantities.TryAdd(product, 0);
        _productQuantities[product] += quantity;
    }

    public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, ISupermarketCatalog catalog)
    {
        foreach (var product in _productQuantities.Keys)
            if (offers.TryGetValue(product, out var offer))
                receipt.AddDiscount(_discountFactory.GetStrategy(offer.OfferType)
                    .Compute(product, offer, _productQuantities[product], catalog.GetUnitPrice(product)));
    }

    public Product FindProductByName(string name)
    {
        var index = _items.FindIndex(item => item.Product.Name == name);
        return _items[index].Product;
    }
}