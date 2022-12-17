using JobApplication.Model.Dto.RoleDto;
using JobApplication.Model.Models;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleService
{
    public interface IRoleService
    {
        Task<RoleMaster> AddRoleAsync(AddRoleDto addRole);
        Task<RoleMaster> GetById(int id);        
    }
}
