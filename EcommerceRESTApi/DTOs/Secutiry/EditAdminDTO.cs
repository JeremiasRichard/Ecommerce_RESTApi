using System.ComponentModel.DataAnnotations;

namespace EcommerceRESTApi.DTOs.Secutiry
{
    public class EditAdminDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
