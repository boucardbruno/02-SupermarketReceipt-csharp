using NFluent;
using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test;

public class OfferTest
{
    [Test]
    public void Should_be_a_value_object()
    {
        var offer1 = new Offer(SpecialOfferType.TenPercentDiscount, 10);
        var offer2 = new Offer(SpecialOfferType.TenPercentDiscount, 10);
        Check.That(offer1).IsEqualTo(offer2);
    }
}