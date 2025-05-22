using System;

namespace Mini_EcoMarket_System
{
    public class Product
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        public Product(string productName, double price, int stock, string category)
        {
            ProductName = productName;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public override string ToString()
        {
            return $"{ProductName} - ${Price} - {Stock} in stock - Category: {Category}";
        }
    }
}
