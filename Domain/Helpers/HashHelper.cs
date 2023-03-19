namespace Domain.Helpers
{
    public class Hash
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public static class HashHelper
    {
        public static string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt))
            );
        }

        public static string GenerateSalt()
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()))
            );
        }


        public static bool VerifyPassword(string password, string salt, string hash)
        {
            return HashPassword(password, salt) == hash;
        }

    }
}
