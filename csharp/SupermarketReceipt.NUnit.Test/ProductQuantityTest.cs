using NFluent;
using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test;

public class ProductQuantityTest
{
    [Test]
    public void Should_be_value_object()
    {
        var riceProduct = new Product("rice", ProductUnit.Each);
        var productQuantity1 = new ProductQuantity(riceProduct, 2);
        var productQuantity2 = new ProductQuantity(riceProduct, 2);
        Check.That(productQuantity1).IsEqualTo(productQuantity2);
    }
}