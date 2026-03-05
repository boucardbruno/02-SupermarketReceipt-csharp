using NFluent;
using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test;

public class DiscountTest
{
    [Test]
    public void Should_be_a_value_object()
    {
        var riceProduct = new Product("rice", ProductUnit.Each);
        var discount1 = new Discount(riceProduct, "rice packet", 2);
        var discount2 = new Discount(riceProduct, "rice packet", 2);
        Check.That(discount1).IsEqualTo(discount2);
    }
}