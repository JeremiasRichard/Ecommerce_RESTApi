namespace EcommerceRESTApi.DTOs.Secutiry
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Id { get; set; }
    }
}
