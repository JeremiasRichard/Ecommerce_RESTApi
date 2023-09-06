using System.ComponentModel.DataAnnotations;

namespace RemitoApi.DTOs.Secutiry
{
    public class EditAdminDTO
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
