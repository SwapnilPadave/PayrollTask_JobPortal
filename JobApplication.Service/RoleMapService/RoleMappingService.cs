using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.RoleMappingDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleMapService
{
    public class RoleMappingService : IRoleMappingService
    {
        private readonly IRoleMappingRepository _roleMappingRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        public RoleMappingService(IRoleMappingRepository roleMappingRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _roleMappingRepository = roleMappingRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<RoleMappingModel> AssignRole(RoleMappingDto roleMapping)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(roleMapping.UserId);
                var role = await _roleRepository.GetByIdAsync(roleMapping.RoleId);
                var data = new RoleMappingModel();
                data.UserId = user.Id;
                data.RoleId = role.Id;
                var result = await _roleMappingRepository.AddAsync(data);

                var userRole = await _userRepository.GetByIdAsync(roleMapping.UserId);
                userRole.RoleId = result.RoleId;
                var userRoleId = _userRepository.UpdateAsync(userRole);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }        
            
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
