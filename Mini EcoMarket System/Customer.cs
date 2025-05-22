using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_EcoMarket_System
{
    public class Customer : User
    {
        public List<Order> OrderHistory { get; private set; } = new List<Order>();
        public Customer(string username, string email) : base(username, email) {}
        
        public void PlaceOrder(Product product, int quantity)
        {
            product.Stock -= quantity;
            Order order = new Order(product.ProductName, quantity, product.Price * quantity);
            OrderHistory.Add(order);
            Console.WriteLine("Order placed successfully.");
        }
        
        public override void DisplayInfo()
        {
            Console.WriteLine($"Customer: {Username}, Email: {Email}, Orders Made: {OrderHistory.Count}");
        }

        public override string GetRole() => "Customer";
    }
}
