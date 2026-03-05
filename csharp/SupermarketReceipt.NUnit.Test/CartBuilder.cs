namespace SupermarketReceipt.NUnit.Test;

internal class CartBuilder
{
    private readonly ShoppingCart _cart = new();

    public CartBuilder AddIemQuantity(string name, ProductUnit productUnit, double quantity)
    {
        _cart.AddItemQuantity(new Product(name, productUnit), quantity);
        return this;
    }

    public ShoppingCart Build()
    {
        return _cart;
    }
}