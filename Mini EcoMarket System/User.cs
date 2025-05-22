using System;

namespace Mini_EcoMarket_System
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        
        public User(string username, string email)
        {
            Username = username;
            Email = email;
        }
        
        public abstract void DisplayInfo();
        public abstract string GetRole();
    }
}
