using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_EcoMarket_System
{
public class Order
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }

    public Order(string productName, int quantity, double totalPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        TotalPrice = totalPrice;
    }

    public override string ToString()
    {
        return $"{Quantity} x {ProductName} - Total: ${TotalPrice}";
    }
}
}
