using System.Collections.Generic;

namespace SupermarketReceipt;

public class Teller(ISupermarketCatalog catalog)
{
    private Dictionary<Product, Offer> Offers { get; } = new();

    public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
    {
        Offers[product] = new Offer(offerType, argument);
    }

    private void AddProductToReceipt(List<CartItem> productQuantities, Receipt receipt)
    {
        foreach (var productQuantity in productQuantities)
            receipt.AddProduct(productQuantity,
                catalog.GetUnitPrice(productQuantity.Product),
                productQuantity.Quantity * catalog.GetUnitPrice(productQuantity.Product));
    }

    public Receipt ChecksOutArticlesFrom(ShoppingCart shoppingCart)
    {
        var receipt = new Receipt();
        AddProductToReceipt(shoppingCart.GetItems(), receipt);
        shoppingCart.HandleOffers(receipt, Offers, catalog);
        return receipt;
    }
}