using JobApplication.Database.Infrastructure;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Database.Repositories
{
    public interface IRoleRepository : IRepository<RoleMaster>
    {

    }
    public class RoleRepository : Repository<RoleMaster>, IRoleRepository
    {
        public RoleRepository(JobApplicationDbContext context) : base(context)
        {

        }    
    }
}
