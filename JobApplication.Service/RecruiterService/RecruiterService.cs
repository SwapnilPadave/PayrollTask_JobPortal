using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Dto.RecruiterDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplication.Service.RecruiterService
{
    public class RecruiterService : IRecruiterService
    {

        private readonly IJobRepository _jobRepository;
        private readonly IRecruiterRepository _recruiterRepository;
        public RecruiterService(IJobRepository job, IRecruiterRepository recruiterRepository)
        {
            _jobRepository = job;
            _recruiterRepository = recruiterRepository;

        }

        public async Task<JobMaster> AddJobAsync(int id, AddJobDto addJobDto)
        {
            try
            {
                if (addJobDto != null)
                {
                    var jobMaster = new JobMaster();
                    jobMaster.Title = addJobDto.Title;
                    jobMaster.Description = addJobDto.Description;
                    jobMaster.CreatedBy = id;
                    jobMaster.CreatedAt = DateTime.Now;
                    jobMaster.isActive = true;
                    return await _jobRepository.AddAsync(jobMaster);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong..");
                throw ex;
            }
        }

        public async Task<IEnumerable<GetJobAppliedByCandidateDto>> GetJobAppliedByCandidateAsync(int id, PaginationModel pagination)
        {
            var appliedJobs = await _recruiterRepository.GetJobAppliedByCandidateAsync(id, pagination);
            if (appliedJobs != null)
            {
                return appliedJobs;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<GetJobDto>> GetPostedJobAsync(int id, PaginationModel pagination)
        {
            var jobs = await _recruiterRepository.GetPostedJobAsync(id, pagination);
            if (jobs != null)
                return jobs;
            return null;
        }
    }
}
