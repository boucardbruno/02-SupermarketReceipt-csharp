using System.Globalization;
using System.Text;

namespace SupermarketReceipt.NUnit.Test;

public class ReceiptPrinter(int columns)
{
    private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");

    public ReceiptPrinter() : this(40)
    {
    }

    public string PrintReceipt(Receipt receipt)
    {
        var result = new StringBuilder();
        foreach (var item in receipt.GetItems())
        {
            var receiptItem = PrintReceiptItem(item);
            result.Append(receiptItem);
        }

        foreach (var discount in receipt.GetDiscounts())
        {
            var discountPresentation = PrintDiscount(discount);
            result.Append(discountPresentation);
        }

        {
            result.Append("\n");
            result.Append(PrintTotal(receipt));
        }
        return result.ToString();
    }

    private string PrintTotal(Receipt receipt)
    {
        var name = "Total: ";
        var value = PrintPrice(receipt.GetTotalPrice());
        return FormatLineWithWhitespace(name, value);
    }

    private string PrintDiscount(Discount discount)
    {
        return FormatLineWithWhitespace(
            discount.Description + 
            "(" + discount.Product.Name + ")", 
            PrintPrice(discount.DiscountAmount));
    }

    private string PrintReceiptItem(ReceiptItem item)
    {
        var totalPrice = PrintPrice(item.TotalPrice);
        var name = item.Product.Name;
        var line = FormatLineWithWhitespace(name, totalPrice);
        if ((int)item.Quantity != 1)
        {
            line += $"  {PrintPrice(item.Price)} * {PrintQuantity(item)}\n";
        }

        return line;
    }


    private string FormatLineWithWhitespace(string name, string value)
    {
        var line = new StringBuilder();
        line.Append(name);
        var whitespaceSize = columns - name.Length - value.Length;
        for (var i = 0; i < whitespaceSize; i++) line.Append(" ");
        line.Append(value);
        line.Append('\n');
        return line.ToString();
    }

    private string PrintPrice(double price)
    {
        return price.ToString("N2", Culture);
    }

    private static string PrintQuantity(ReceiptItem item)
    {
        return ProductUnit.Each == item.Product.Unit
            ? ((int)item.Quantity).ToString()
            : item.Quantity.ToString("N3", Culture);
    }
}