using NFluent;
using NUnit.Framework;

namespace SupermarketReceipt.NUnit.Test;

public class ReceiptItemTest
{
    [Test]
    public void Should_be_a_value_object()
    {
        var riceProduct = new Product("rice", ProductUnit.Each);
        var receiptItem1 = new ReceiptItem(riceProduct, 2, 0.5, 5.0);
        var receiptItem2 = new ReceiptItem(riceProduct, 2, 0.5, 5.0);
        Check.That(receiptItem1).IsEqualTo(receiptItem2);
    }
}