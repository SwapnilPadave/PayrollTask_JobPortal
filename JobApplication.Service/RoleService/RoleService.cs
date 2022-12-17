using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.RoleDto;
using JobApplication.Model.Models;
using System;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        public RoleService(IRoleRepository role, IUserRepository userRepository)
        {
            _roleRepository = role;
            _userRepository = userRepository;
        }

        public async Task<RoleMaster> AddRoleAsync(AddRoleDto addRole)
        {
            try
            {
                var data = new RoleMaster();
                if (data != null)
                {
                    data.Role = addRole.Role;
                    await _roleRepository.AddAsync(data);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<RoleMaster> GetById(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role != null)
                return role;
            return null;
        }
    }
}
