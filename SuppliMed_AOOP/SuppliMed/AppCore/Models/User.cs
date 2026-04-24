namespace AppCore.Models
{
    public abstract class User
    {
        public required string UserID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public UserRole Role { get; set; }
        public abstract string GetRole();

        public bool Authenticate(string username, string password)
        {
            return Username == username && Password == password;
        }
    }
}