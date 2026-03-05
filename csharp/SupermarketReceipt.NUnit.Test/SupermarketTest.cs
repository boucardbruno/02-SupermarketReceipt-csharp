using NFluent;
using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test;

public class SupermarketTest
{
    private const string Toothbrush = "toothbrush";
    private const string Apples = "apples";
    private const string Rice = "rice";
    private const string CherryTomatoes = "cherry tomatoes";

    [Test]
    public void Should_print_receipt_with_apples_discount_when_teller_offer_ten_percent()
    {
        // ARRANGE
        var catalog = new CatalogBuilder()
            .AddProduct(Toothbrush, 0.99, ProductUnit.Each)
            .AddProduct(Apples, 1.99, ProductUnit.Kilo)
            .AddProduct(Rice, 2.99, ProductUnit.Each)
            .AddProduct(CherryTomatoes, 0.99, ProductUnit.Each)
            .Build();

        var cart = new CartBuilder()
            .AddIemQuantity(Apples, ProductUnit.Kilo, 5)
            .AddIemQuantity(Toothbrush, ProductUnit.Each, 5)
            .AddIemQuantity(Rice, ProductUnit.Each, 2)
            .AddIemQuantity(CherryTomatoes, ProductUnit.Each, 2)
            .Build();

        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, cart.FindProductByName(Apples), 5.0);

        // ACT
        var receipt = teller.ChecksOutArticlesFrom(cart);

        // ASSERT
        Check.That(receipt.GetTotalPrice()).IsEqualTo(22.36);
        Check.That(receipt.GetDiscounts()).HasSize(1);
        Check.That(receipt.GetItems().Count).IsEqualTo(4);
        var receiptItemForRice = receipt.GetReceiptItemByName(Apples);
        Check.That(receiptItemForRice.Product).IsEqualTo(cart.FindProductByName(Apples));
        Check.That(receiptItemForRice.Price).IsEqualTo(1.99);
        Check.That(receiptItemForRice.TotalPrice).IsEqualTo(5 * 1.99);
        Check.That(receiptItemForRice.Quantity).IsEqualTo(5);

        Check.That(new ReceiptPrinter().PrintReceipt(receipt))
            .IsEqualTo(
                "apples                              9.95\n" +
                "  1.99 * 5.000\n" +
                "toothbrush                          4.95\n" +
                "  0.99 * 5\n" +
                "rice                                5.98\n" +
                "  2.99 * 2\n" +
                "cherry tomatoes                     1.98\n" +
                "  0.99 * 2\n" +
                "5% off(apples)                     -0.50\n" +
                "\n" +
                "Total:                             22.36\n");
    }

    [Test]
    public void Should_print_receipt_with_toothbrush_discount_when_teller_offer_five()
    {
        // ARRANGE
        var catalog = new CatalogBuilder()
            .AddProduct(Toothbrush, 0.99, ProductUnit.Each)
            .AddProduct(Apples, 1.99, ProductUnit.Kilo)
            .AddProduct(Rice, 2.99, ProductUnit.Each)
            .AddProduct(CherryTomatoes, 0.99, ProductUnit.Each)
            .Build();

        var cart = new CartBuilder()
            .AddIemQuantity(Apples, ProductUnit.Kilo, 7.69)
            .AddIemQuantity(Toothbrush, ProductUnit.Each, 5).Build();

        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, cart.FindProductByName(Toothbrush), 8.0);

        // ACT
        var receipt = teller.ChecksOutArticlesFrom(cart);

        // ASSERT
        Check.That(receipt.GetTotalPrice()).IsEqualTo(23.3);
        Check.That(receipt.GetDiscounts().Count).IsEqualTo(1);
        Check.That(receipt.GetItems().Count).IsEqualTo(2);
        var receiptItemForToothbrush = receipt.GetReceiptItemByName(Toothbrush);
        Check.That(receiptItemForToothbrush.Product.Name)
            .IsEqualTo(cart.FindProductByName(Toothbrush).Name);
        Check.That(receiptItemForToothbrush.Price).IsEqualTo(0.99);
        Check.That(receiptItemForToothbrush.TotalPrice).IsEqualTo(5 * 0.99);
        Check.That(receiptItemForToothbrush.Quantity).IsEqualTo(5);
        var printReceipt = new ReceiptPrinter().PrintReceipt(receipt);
        Check.That(printReceipt)
            .IsEqualTo(
                "apples                             15.30\n" +
                "  1.99 * 7.690\n" +
                "toothbrush                          4.95\n" +
                "  0.99 * 5\n" +
                "5 for 8.00(toothbrush)              3.05\n" +
                "\n" +
                "Total:                             23.30\n");
    }

    [Test]
    public void Should_print_receipt_with_discount_for_cherry_tomatoes_when_teller_offer_three_for_two()
    {
        // ARRANGE
        var catalog = new CatalogBuilder()
            .AddProduct(Toothbrush, 0.99, ProductUnit.Each)
            .AddProduct(Apples, 1.99, ProductUnit.Kilo)
            .AddProduct(Rice, 2.99, ProductUnit.Each)
            .AddProduct(CherryTomatoes, 0.99, ProductUnit.Each)
            .Build();

        var cart = new CartBuilder()
            .AddIemQuantity(Apples, ProductUnit.Kilo, 0.99 + 2.5 + 3.2)
            .AddIemQuantity(Toothbrush, ProductUnit.Each, 5)
            .AddIemQuantity(CherryTomatoes, ProductUnit.Each, 10)
            .Build();

        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, cart.FindProductByName(CherryTomatoes), 10.0);

        // ACT
        var receipt = teller.ChecksOutArticlesFrom(cart);

        // ASSERT
        Check.That(receipt.GetTotalPrice()).IsEqualTo(25.85);
        Check.That(receipt.GetDiscounts()).HasSize(1);
        Check.That(receipt.GetItems()).HasSize(3);
        var receiptItemForCherryTomatoes = receipt.GetReceiptItemByName(CherryTomatoes);
        Check.That(receiptItemForCherryTomatoes.Product).IsEqualTo(cart.FindProductByName(CherryTomatoes));
        Check.That(receiptItemForCherryTomatoes.Price).IsEqualTo(0.99);
        Check.That(receiptItemForCherryTomatoes.TotalPrice).IsEqualTo(10 * 0.99);
        Check.That(receiptItemForCherryTomatoes.Quantity).IsEqualTo(10);

        Check.That(new ReceiptPrinter().PrintReceipt(receipt))
            .IsEqualTo(
                "apples                             13.31\n" +
                "  1.99 * 6.690\n" +
                "toothbrush                          4.95\n" +
                "  0.99 * 5\n" +
                "cherry tomatoes                     9.90\n" +
                "  0.99 * 10\n" +
                "3 for 2(cherry tomatoes)           -2.31\n" +
                "\n" +
                "Total:                             25.85\n"
            );
    }

    [Test]
    public void Should_print_receipt_with_discount_for_rice_when_teller_offer_two_for_amount_discount()
    {
        // ARRANGE
        var catalog = new CatalogBuilder()
            .AddProduct(Toothbrush, 0.99, ProductUnit.Each)
            .AddProduct(Apples, 1.99, ProductUnit.Kilo)
            .AddProduct(Rice, 2.99, ProductUnit.Each)
            .AddProduct(CherryTomatoes, 0.99, ProductUnit.Each)
            .Build();

        var cart = new CartBuilder()
            .AddIemQuantity(Apples, ProductUnit.Kilo, 4.5)
            .AddIemQuantity(Rice, ProductUnit.Each, 8).Build();

        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, cart.FindProductByName(Rice), 8);

        // ACT
        var receipt = teller.ChecksOutArticlesFrom(cart);

        // ASSERT
        Check.That(receipt.GetTotalPrice()).IsEqualTo(40.96);
        Check.That(receipt.GetDiscounts()).HasSize(1);
        Check.That(receipt.GetItems().Count).IsEqualTo(2);
        var receiptItemForRice = receipt.GetReceiptItemByName(Rice);
        Check.That(receiptItemForRice.Product).IsEqualTo(cart.FindProductByName(Rice));
        Check.That(receiptItemForRice.Price).IsEqualTo(2.99);
        Check.That(receiptItemForRice.TotalPrice).IsEqualTo(8 * 2.99);
        Check.That(receiptItemForRice.Quantity).IsEqualTo(8);
        Check.That(new ReceiptPrinter().PrintReceipt(receipt))
            .IsEqualTo(
                "apples                              8.96\n" +
                "  1.99 * 4.500\n" +
                "rice                               23.92\n" +
                "  2.99 * 8\n" +
                "2 for 8.00(rice)                    8.08\n" +
                "\n" +
                "Total:                             40.96\n");
    }
    
    [Test]
    public void Should_print_receipt_with_all_discounts_when_teller_is_foolish()
    {
        // ARRANGE
        var catalog = new CatalogBuilder()
            .AddProduct(Toothbrush, 0.99, ProductUnit.Each)
            .AddProduct(Apples, 1.99, ProductUnit.Kilo)
            .AddProduct(Rice, 2.99, ProductUnit.Each)
            .AddProduct(CherryTomatoes, 0.99, ProductUnit.Each)
            .Build();

        var cart = new CartBuilder()
            .AddIemQuantity(Apples, ProductUnit.Kilo, 5)
            .AddIemQuantity(Toothbrush, ProductUnit.Each, 5)
            .AddIemQuantity(Rice, ProductUnit.Each, 2)
            .AddIemQuantity(CherryTomatoes, ProductUnit.Each, 2)
            .Build();

        var teller = new Teller(catalog);
        teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, cart.FindProductByName(CherryTomatoes), 10.0);
        teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, cart.FindProductByName(Toothbrush), 8.0);
        teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, cart.FindProductByName(Rice), 8);
        teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, cart.FindProductByName(Apples), 5.0);

        // ACT
        var receipt = teller.ChecksOutArticlesFrom(cart);

        // ASSERT
        Check.That(receipt.GetTotalPrice()).IsEqualTo(28.75);
        Check.That(receipt.GetDiscounts()).HasSize(4);
        Check.That(receipt.GetItems().Count).IsEqualTo(4);
        var receiptItemForRice = receipt.GetReceiptItemByName(Apples);
        Check.That(receiptItemForRice.Product).IsEqualTo(cart.FindProductByName(Apples));
        Check.That(receiptItemForRice.Price).IsEqualTo(1.99);
        Check.That(receiptItemForRice.TotalPrice).IsEqualTo(5 * 1.99);
        Check.That(receiptItemForRice.Quantity).IsEqualTo(5);
        Check.That(new ReceiptPrinter().PrintReceipt(receipt))
            .IsEqualTo(
                "apples                              9.95\n" +
                       "  1.99 * 5.000\n" +
                       "toothbrush                          4.95\n" +
                       "  0.99 * 5\n" +
                       "rice                                5.98\n" +
                       "  2.99 * 2\n" +
                       "cherry tomatoes                     1.98\n" +
                       "  0.99 * 2\n" +
                       "5% off(apples)                     -0.50\n" +
                       "5 for 8.00(toothbrush)              3.05\n" +
                       "2 for 8.00(rice)                    2.02\n" +
                       "3 for 2(cherry tomatoes)            1.32\n" +
                       "\n" +
                       "Total:                             28.75\n");
    }

}