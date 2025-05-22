using System;
using System.Collections.Generic;
using System.IO;

namespace Mini_EcoMarket_System
{
    class Program
    {
        static Farmer defaultFarmer = new Farmer("JohnDoe", "john@example.com");
        static Customer defaultCustomer = new Customer("JaneSmith", "jane@example.com");
        static List<Product> AllProducts = new List<Product>();
        static User accountLoggedIn;
        static void Main()
        {
            LoadData();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Mini EcoMarket ---");
                Console.WriteLine("1. List all products");
                Console.WriteLine("2. Customer account");
                Console.WriteLine("3. Farmer account");
                Console.WriteLine("0. Save and exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListProducts();
                        break;
                    case "2":
                        CustomerAccount();
                        break;
                    case "3":
                        FarmerAccount();
                        break;

                    case "0":
                        SaveData();
                        Console.WriteLine("Data saved. Exiting...");
                        return;
                    
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
                if (choice != "0") // Don't pause if exiting
                {
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
        }

        static void CustomerAccount()
        {
            accountLoggedIn = defaultCustomer;
            
            while (true) {
                Console.Clear();
                Console.WriteLine($"\n--- Customer Account: {accountLoggedIn.Username} ---");
                Console.WriteLine("1. List all products");
                Console.WriteLine("2. Buy a product");
                Console.WriteLine("3. View order history");
                Console.WriteLine("4. View account information");
                Console.WriteLine("0. Logout");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListProducts();
                        break;
                    case "2":
                        BuyProduct();
                        break;
                    case "3":
                        ViewOrderHistory();
                        break;
                    case "4":
                        Console.WriteLine("\n--- Your Account Information ---");
                        accountLoggedIn.DisplayInfo();
                        break;
                    case "0":
                        Console.WriteLine($"{accountLoggedIn.Username} logging out...");
                        accountLoggedIn = null;
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
                
                
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                
            }
            
        }
        
        static void FarmerAccount()
        {
            accountLoggedIn = defaultFarmer;
            
            while(true) {
                Console.Clear();
                Console.WriteLine($"\n--- Farmer Account: {accountLoggedIn.Username} ---");
                Console.WriteLine("1. List all products in market");
                Console.WriteLine("2. Add new product");
                Console.WriteLine("3. View account information");
                Console.WriteLine("0. Logout");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ListProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        Console.WriteLine("\n--- Your Account Information ---");
                        accountLoggedIn.DisplayInfo();
                        break;
                    case "0":
                        Console.WriteLine($"{accountLoggedIn.Username} logging out...");
                        accountLoggedIn = null;
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            
        }
        static void LoadData()
        {
            if (!File.Exists("products.txt") || !File.Exists("orders.txt"))
            {
                defaultFarmer.AddProduct(new Product("Tomatoes", 2.5, 100, "Vegetables"));
                defaultFarmer.AddProduct(new Product("Apples", 3.0, 50, "Fruits"));
                
                AllProducts.AddRange(defaultFarmer.Products);
                Console.WriteLine("Default data loaded.");
                return;
            }

            // Load products
            try
            {
                var productLines = File.ReadAllLines("products.txt");
                foreach (var line in productLines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        var product = new Product(parts[0], double.Parse(parts[1]), int.Parse(parts[2]), parts[3]);
                        defaultFarmer.AddProduct(product);
                        AllProducts.Add(product);
                    }
                }

                Console.WriteLine("Products loaded from file.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load products: " + e.Message);
            }

            // Load orders
            try
            {
                var orderLines = File.ReadAllLines("orders.txt");
                foreach (var line in orderLines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        var order = new Order(parts[0], int.Parse(parts[1]), double.Parse(parts[2]));
                        defaultCustomer.OrderHistory.Add(order);
                    }
                }
                Console.WriteLine("Orders loaded from file.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load orders: " + e.Message);
            }
        }

        static void ListProducts()
        {
            Console.WriteLine("\nAvailable Products:");
            for (int i = 0; i < AllProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {AllProducts[i]}");
            }
        }

        static void BuyProduct()
        {

            ListProducts();
            try
            {
                Console.Write("Enter product ID to buy: ");
            if (!int.TryParse(Console.ReadLine(), out int prodIndex) || prodIndex < 1 || prodIndex > AllProducts.Count)
            {
                throw new InvalidProductIDException($"Invalid product ID.");
            }
            var product = AllProducts[prodIndex - 1];

            Console.Write("Enter quantity to buy: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 1)
            {
                throw new InventoryException($"Invalid quantity.");
            }
            else if (quantity > product.Stock)
            {
                throw new InventoryException($"Not enough stock available.");
            } 
            defaultCustomer.PlaceOrder(product, quantity);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        static void ViewOrderHistory()
        {

            Console.WriteLine($"\nOrder history for {defaultCustomer.Username}:");
            if (defaultCustomer.OrderHistory.Count == 0)
            {
                Console.WriteLine("No orders made yet.");
                return;
            }
            foreach (var order in defaultCustomer.OrderHistory)
            {
                Console.WriteLine(order);
            }
        }

        static void AddProduct()
        {
            try
            {
                Console.Write("Enter product name: ");
                string name = Console.ReadLine();

                Console.Write("Enter price: ");
                if (!double.TryParse(Console.ReadLine(), out double price))
                {
                    throw new InvalidPriceException("Invalid price.");
                }
                else if (price <= 0)
                {
                    throw new InvalidPriceException("Price must be positive.");
                }

                Console.Write("Enter stock quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int stock))
                {
                    throw new InvalidStockQuantityException("Invalid stock quantity.");
                }
                else if (stock <= 0)
                {
                    throw new InvalidStockQuantityException("Stock must be positive.");
                }

                Console.Write("Enter category: ");
                string category = Console.ReadLine();

                var product = new Product(name, price, stock, category);
                defaultFarmer.AddProduct(product);
                AllProducts.Add(product);
                Console.WriteLine("Product added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            

        }

        static void SaveData()
        {
            SaveProductsToFile("products.txt", AllProducts);
            if (defaultCustomer != null)
            {
                SaveOrdersToFile("orders.txt", defaultCustomer.OrderHistory);
            }
        }

        static void SaveProductsToFile(string filePath, List<Product> products)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                    foreach (var product in products)
                    {
                        writer.WriteLine($"{product.ProductName},{product.Price},{product.Stock},{product.Category}");
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save products: " + ex.Message);
            }
        }

        static void SaveOrdersToFile(string filePath, List<Order> orders)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"{order.ProductName},{order.Quantity},{order.TotalPrice}");
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save orders: " + ex.Message);
            }
        }
    }
}
