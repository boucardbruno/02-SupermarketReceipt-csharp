using System.Collections.Generic;

namespace SupermarketReceipt.DiscountStrategy;

public class StrategyDiscountFactory
{
    private readonly Dictionary<SpecialOfferType, IProvideDiscount> _discountStrategy = new()
    {
        { SpecialOfferType.ThreeForTwo, new DiscountTreeForTwo() },
        { SpecialOfferType.TwoForAmount, new DiscountTwoForAmount() },
        { SpecialOfferType.FiveForAmount, new DiscountFiveForAmount() },
        { SpecialOfferType.TenPercentDiscount, new DiscountTenPercent() }
    };


    public IProvideDiscount GetStrategy(SpecialOfferType offerOfferType)
    {
        return _discountStrategy[offerOfferType];
    }
}