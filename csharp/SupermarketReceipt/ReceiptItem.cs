using System;
using System.Collections.Generic;
using Value;

namespace SupermarketReceipt;

public class ReceiptItem(Product product, double quantity, double price, double totalPrice) : ValueType<ReceiptItem>
{
    public Product Product { get; } = product;
    public double Quantity { get; } = quantity;
    public double Price { get; } = Math.Round(price, 2);
    public double TotalPrice { get; } = Math.Round(totalPrice, 2);

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return [Product, Quantity, Price, TotalPrice];
    }
}