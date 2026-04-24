namespace AppCore.Models
{
    public abstract class User
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserRole Role { get; set; }
        public abstract string GetRole();

        public bool Authenticate(string username, string password)
        {
            return Username == username && Password == password;
        }
    }
}