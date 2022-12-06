using JobApplication.Database.Infrastructure;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Dto.RecruiterDto;
using JobApplication.Model.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Database.Repositories
{
    public interface IRecruiterRepository
    {
        Task<IEnumerable<GetJobAppliedByCandidateDto>> GetJobAppliedByCandidateAsync(int id, PaginationModel pagination);

        Task<IEnumerable<GetJobDto>> GetPostedJobAsync(int id, PaginationModel pagination);
    }
    public class RecruiterRepository : Repository<UserMaster>, IRecruiterRepository
    {
        public RecruiterRepository(JobApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<GetJobAppliedByCandidateDto>> GetJobAppliedByCandidateAsync(int id, PaginationModel pagination)
        {

            var AppliedJobs = await (from u in _context.User
                                     join a in _context.candidateMasters on u.Id equals a.CandidateId
                                     join j in _context.jobMasters on a.AppliedJobId equals j.Id
                                     where j.CreatedBy == id
                                     select new GetJobAppliedByCandidateDto
                                     {
                                         Id = u.Id,
                                         CandidateName = u.Name,
                                         JobTitle = j.Title,
                                         Description = j.Description,
                                         AppliedAt = a.AppliedAt

                                     })
                               .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .OrderBy(x => x.Id)
                                .ToListAsync();
            return AppliedJobs;

        }


        public async Task<IEnumerable<GetJobDto>> GetPostedJobAsync(int id, PaginationModel pagination)
        {
            var Jobs = await (from j in _context.jobMasters
                              where j.CreatedBy == id
                              select new GetJobDto
                              {
                                  Id = j.Id,
                                  Title = j.Title,
                                  Description = j.Description
                              }).Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                .Take(pagination.PageSize)
                                .OrderBy(x => x.Id)
                                .ToListAsync();


            return Jobs;
        }
    }
}