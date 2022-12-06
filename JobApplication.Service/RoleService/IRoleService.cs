using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleService
{
    public interface IRoleService
    {
        Task<RoleMaster> AddRoleAsync();
        Task<RoleMaster> GetById(int id);
    }
}
