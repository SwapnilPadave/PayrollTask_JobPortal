using JobApplication.Model.Dto.RoleMappingDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleMapService
{
     public interface IRoleMappingService
    {
        Task<RoleMappingModel> AssignRole(RoleMappingDto roleMapping);
        Task<IEnumerable<RoleMappingModel>> GetAllRoleMapping();
        Task<RoleMappingModel> GetRoleMappingById(int id);
    }
}
