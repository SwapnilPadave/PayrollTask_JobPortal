using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobApplication.Model.Dto.RoleDto
{
    public class AddRoleDto
    {
        [Required]
        public string Role { get; set; }
    }
}
