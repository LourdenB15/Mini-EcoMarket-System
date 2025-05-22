using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_EcoMarket_System
{
    public class Farmer : User
    {
        public List<Product> Products { get; private set; } = new List<Product>();
        public Farmer(string username, string email) : base(username, email) { }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Farmer: {Username}, Email: {Email}, Products Listed: {Products.Count}");
        }

        public override string GetRole() => "Farmer";
    }
}
