using System.ComponentModel.DataAnnotations;

namespace BackendApi2.Entities.DTOModels;
public class LoginUserDto
{
        [Required,MaxLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
}
