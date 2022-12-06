using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Dto.RecruiterDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.RecruiterService
{
    public interface IRecruiterService
    {
        Task<IEnumerable<GetJobDto>> GetPostedJobAsync(int id, PaginationModel pagination);

        Task<IEnumerable<GetJobAppliedByCandidateDto>> GetJobAppliedByCandidateAsync(int id, PaginationModel pagination);

        Task<JobMaster> AddJobAsync(int id, AddJobDto addJobDto);
    }
}
