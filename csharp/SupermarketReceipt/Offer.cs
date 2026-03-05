using System.Collections.Generic;
using Value;

namespace SupermarketReceipt;

public class Offer(SpecialOfferType offerType, double argument) : ValueType<Offer>
{
    public SpecialOfferType OfferType { get; } = offerType;
    public double Argument { get; } = argument;

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return [OfferType, Argument];
    }
}