using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt;

public class Receipt
{
    private readonly List<Discount> _discounts = [];
    private readonly List<ReceiptItem> _items = [];

    public double GetTotalPrice()
    {
        var totalPrice = _items.Sum(item => item.TotalPrice) + _discounts.Sum(discount => discount.DiscountAmount);
        return Math.Round(totalPrice, 2);
    }

    public void AddProduct(ProductQuantity productQuantity, double price, double totalPrice)
    {
        _items.Add(new ReceiptItem(productQuantity.Product, productQuantity.Quantity, price, totalPrice));
    }

    public List<ReceiptItem> GetItems()
    {
        return [.._items];
    }

    public void AddDiscount(Discount discount)
    {
        _discounts.Add(discount);
    }

    public List<Discount> GetDiscounts()
    {
        return _discounts;
    }

    public ReceiptItem GetReceiptItemByName(string name)
    {
        return _items.First(item => item.Product.Name == name);
    }
}