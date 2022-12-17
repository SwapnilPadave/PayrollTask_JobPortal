using JobApplication.Model.Models;

namespace JobApplication.Database.Infrastructure
{
    public interface IAccountRepository : IRepository<UserMaster>
    {

    }
    public class AccountRepository : Repository<UserMaster>, IAccountRepository
    {
        public AccountRepository(JobApplicationDbContext context) : base(context)
        {

        }
    }
}
