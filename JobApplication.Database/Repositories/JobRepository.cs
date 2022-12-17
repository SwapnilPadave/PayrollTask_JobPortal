using JobApplication.Database.Infrastructure;
using JobApplication.Model.Dto.CandidateDto;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Database.Repositories
{
    public interface IJobRepository : IRepository<JobMaster>
    {
        Task<IEnumerable<GetCandidateDto>> GetJobsApplied(int id, PaginationModel pagination);
        Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination);
    }
    public class JobRepository : Repository<JobMaster>, IJobRepository
    {
        public JobRepository(JobApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<GetCandidateDto>> GetJobsApplied(int id, PaginationModel pagination)
        {
            var jobApplied = await (from j in _context.jobMasters
                                    join a in _context.candidateMasters on j.Id equals a.AppliedJobId
                                    join u in _context.User on a.CandidateId equals u.Id
                                    where a.CandidateId == id
                                    select new GetCandidateDto
                                    {
                                        Id = u.Id,
                                        CandidateName = u.Name,
                                        JobTitle = j.Title,
                                        JobDescription = j.Description,
                                        AppliedAt = a.AppliedAt
                                    }).Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                 .Take(pagination.PageSize)
                                 .OrderBy(x => x.Id)
                                 .ToListAsync();
            return jobApplied;

        }


        public async Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination)
        {
            var jobs = await (from j in _context.jobMasters
                              select new GetJobDto
                              {
                                  Id = j.Id,
                                  Title = j.Title,
                                  Description = j.Description
                              }).Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                 .Take(pagination.PageSize)
                                 .OrderBy(x => x.Id)
                                 .ToListAsync();

            return jobs;
        }
    }
}