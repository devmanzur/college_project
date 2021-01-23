namespace Snapkart.Domain.Dto.Response
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public int Expire { get; set; }
        public string UserRole { get; set; }
    }
}