namespace AppCore.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual string GetRole()
        {
            return "User";
        }

        public bool Authenticate(string username, string password)
        {
            return Username == username && Password == password;
        }
    }
}