using System;

namespace Mini_EcoMarket_System
{
    public class InventoryException : Exception
    {
        public InventoryException(string message) : base(message) { }
    }
    public class InvalidProductIDException : Exception
    {
        public InvalidProductIDException(string message) : base(message) { }
    }
    class InvalidStockQuantityException : Exception
    {
        public InvalidStockQuantityException(string message) : base(message) { }
    }
    class InvalidPriceException : Exception
    {
        public InvalidPriceException(string message) : base(message) { }
    }
}
