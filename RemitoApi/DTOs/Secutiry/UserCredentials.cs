using System.ComponentModel.DataAnnotations;

namespace RemitoApi.DTOs.Secutiry
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
