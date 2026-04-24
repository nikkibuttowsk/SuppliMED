namespace AppCore.Models
{
    public class Staff : User
    {
        public override string GetRole()
        {
            return "Staff";
        }
    }
}