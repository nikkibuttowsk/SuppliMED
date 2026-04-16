using System.Collections.Generic;
using System.Linq;
using AppCore.Models;

public static class AuthService
{
    private static List<User> users = new List<User>
    {
        new Admin { Username = "admin", Password = "admin123" },
        new Staff { Username = "staff", Password = "staff123" }
    };

    public static User Login(string username, string password)
    {
        return users.FirstOrDefault(u =>
            u.Username == username && u.Password == password);
    }
}