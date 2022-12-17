using JobApplication.Database.Infrastructure;
using JobApplication.Model.Models;
namespace JobApplication.Database.Repositories
{
    public interface IRoleMappingRepository : IRepository<RoleMappingModel>
    {
        
    }
    public class RoleMappingRepository :Repository<RoleMappingModel>, IRoleMappingRepository
    {
        public RoleMappingRepository(JobApplicationDbContext context) : base(context)
        {

        }
    }
}
