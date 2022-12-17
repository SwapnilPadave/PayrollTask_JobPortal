using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Model.Dto.UserDto
{
    public class AddUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Minimum length should be 8 characters,1 capital letter,1 special character,1 numeric character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
