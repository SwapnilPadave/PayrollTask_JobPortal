using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.RoleMappingDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleMapService
{
    public class RoleMappingService : IRoleMappingService
    {
        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RoleMappingService(IRoleMappingRepository roleMappingRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleMappingRepository = roleMappingRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<RoleMappingModel> AssignRole(RoleMappingDto roleMapping)
        {
            var data = new RoleMappingModel();
            var role = await _roleRepository.GetByIdAsync(roleMapping.RoleId);
            var user = await _userRepository.GetByIdAsync(roleMapping.UserId);
            data.RoleId = role.Id;
            data.UserId = user.Id;
            var result = await _roleMappingRepository.AddAsync(data);            
            return result;
        }

        public async Task<IEnumerable<RoleMappingModel>> GetAllRoleMapping()
        {
            var data = await _roleMappingRepository.GetAsync();
            return data;
        }

        public async Task<RoleMappingModel> GetRoleMappingById(int id)
        {
            var data = await _roleMappingRepository.GetByIdAsync(id);
            return data;
        }
    }
}
