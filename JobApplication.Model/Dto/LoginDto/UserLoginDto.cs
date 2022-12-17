using System.ComponentModel.DataAnnotations;

namespace JobApplication.Model.Dto.LoginDto
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
