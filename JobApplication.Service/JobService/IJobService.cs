using JobApplication.Model.Dto.CandidateDto;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.JobService
{
    public interface IJobService
    {
        Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination);

        Task<IEnumerable<GetCandidateDto>> GetJobsApplied(int id, PaginationModel pagination);

        Task<CandidateMaster> ApplyJobsAsync(int userId, JobApplyDto job);
    }
}
