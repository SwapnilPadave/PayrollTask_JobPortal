using JobApplication.Database.Infrastructure;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Database.Repositories
{
    public interface ICandidateRepository : IRepository<CandidateMaster>
    {
    }
    public class CandidateRepository : Repository<CandidateMaster>, ICandidateRepository
    {
        public CandidateRepository(JobApplicationDbContext context) : base(context)
        {

        }
    }
}
