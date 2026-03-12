using NFluent;
using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test;

public class CartItemTest
{
    [Test]
    public void Should_be_value_object()
    {
        var riceProduct = new Product("rice", ProductUnit.Each);
        var productQuantity1 = new CartItem(riceProduct, 2);
        var productQuantity2 = new CartItem(riceProduct, 2);
        Check.That(productQuantity1).IsEqualTo(productQuantity2);
    }
}