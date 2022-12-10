using JobApplication.Model.Dto.RoleDto;
using JobApplication.Model.Dto.RoleMappingDto;
using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleService
{
    public interface IRoleService
    {
        Task<RoleMaster> AddRoleAsync(AddRoleDto addRole);
        Task<RoleMaster> GetById(int id);        
    }
}
