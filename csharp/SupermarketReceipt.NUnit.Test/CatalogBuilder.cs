namespace SupermarketReceipt.NUnit.Test;

internal class CatalogBuilder
{
    private readonly ISupermarketCatalog _catalog = new FakeCatalog();

    public CatalogBuilder AddProduct(string productName, double price, ProductUnit productUnit)
    {
        _catalog.AddProduct(new Product(productName, productUnit), price);
        return this;
    }

    public ISupermarketCatalog Build()
    {
        return _catalog;
    }
}