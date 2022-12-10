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

        public RoleMappingService(IRoleMappingRepository roleMappingRepository)
        {
            _roleMappingRepository = roleMappingRepository;
        }

        public async Task<RoleMappingModel> AssignRole(RoleMappingDto roleMapping)
        {
            var data = new RoleMappingModel();            
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
