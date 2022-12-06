using JobApplication.Database.Repositories;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository role)
        {
            _roleRepository = role;
        }

        public async Task<RoleMaster> AddRoleAsync()
        {
            try
            {
                var data = new RoleMaster();
                if (data != null)
                {                    
                    await _roleRepository.AddAsync(data);
                }
                return data;
            }
            catch(Exception ex)
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
