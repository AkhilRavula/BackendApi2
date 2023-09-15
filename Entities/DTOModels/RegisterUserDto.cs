using System.ComponentModel.DataAnnotations;

namespace BackendApi2.Entities.DTOModels;
public class RegisterUserDto
{
        [Required]
        public string Username { get; set; } = null!;

         [Required]
        public string Password { get; set; } = null!;

         [Required]
        public string Role { get; set; } = null!;
}
