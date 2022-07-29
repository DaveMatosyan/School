namespace School_Project.Services
{
    public class HashServices
    {
        static public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        static public bool VerifyPassword(string DbPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, DbPassword);
        }
    }
}
