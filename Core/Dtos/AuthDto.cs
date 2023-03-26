namespace Core.Dtos
{
    public class AuthDto
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
