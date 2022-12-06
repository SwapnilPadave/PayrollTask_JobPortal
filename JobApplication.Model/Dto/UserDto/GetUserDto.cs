using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Model.Dto.UserDto
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
