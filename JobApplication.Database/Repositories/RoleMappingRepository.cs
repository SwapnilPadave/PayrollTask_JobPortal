using JobApplication.Database.Infrastructure;
using JobApplication.Model.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Database.Repositories
{
    public interface IRoleMappingRepository : IRepository<RoleMappingModel>
    {
        //Task<RoleMappingModel> GetRoleMapByUserId(int userId);
    }
    public class RoleMappingRepository :Repository<RoleMappingModel>, IRoleMappingRepository
    {
        public RoleMappingRepository(JobApplicationDbContext context) : base(context)
        {

        }

        //public Task<RoleMappingModel> GetRoleMapByUserId(int userId)
        //{
        //    var data = (from u in _context.User
        //                      join r in _context.roleMasters on u.RoleId equals r.Id
        //                      join rm in _context.roleMappingModels on r.Id equals rm.RoleId                              
        //                      select new RoleMappingModel
        //                      {
        //                          RoleId=r.Id,
        //                          UserId=rm.UserId
        //                      }).FirstOrDefault();
        //    return data;
        //}
    }
}
