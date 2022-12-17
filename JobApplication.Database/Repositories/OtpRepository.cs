using JobApplication.Database.Infrastructure;
using JobApplication.Model;
namespace JobApplication.Database.Repositories
{
    public interface IOtpRepository : IRepository<OtpMaster>
    {
    }
    public class OtpRepository : Repository<OtpMaster>, IOtpRepository
    {
        public OtpRepository(JobApplicationDbContext context) : base(context)
        {

        }
    
    }
}
