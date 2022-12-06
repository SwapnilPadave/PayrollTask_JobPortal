using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
