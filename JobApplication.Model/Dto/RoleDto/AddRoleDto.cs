using System.ComponentModel.DataAnnotations;

namespace JobApplication.Model.Dto.RoleDto
{
    public class AddRoleDto
    {
        [Required]
        public string Role { get; set; }
    }
}
