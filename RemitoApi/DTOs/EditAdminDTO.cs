using System.ComponentModel.DataAnnotations;

namespace RemitoApi.DTOs
{
    public class EditAdminDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
