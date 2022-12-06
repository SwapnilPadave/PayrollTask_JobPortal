using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.CandidateDto;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using JobApplication.Service.EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.JobService
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        public readonly IUserRepository _userRepository;
        public readonly IEmailService _mailservice;

        public JobService(IJobRepository jobRepository, ICandidateRepository candidateRepository, IUserRepository userRepository, IEmailService emailService)
        {
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _userRepository = userRepository;
            _mailservice = emailService;

        }
        public async Task<CandidateMaster> ApplyJobsAsync(int userId, JobApplyDto job)
        {
            var applyjobs = new CandidateMaster();
            applyjobs.CandidateId = userId;
            applyjobs.AppliedJobId = job.JobId;
            applyjobs.AppliedAt = DateTime.Now;
            if (job != null)
            {
                var candidatedetail = await _userRepository.GetByIdAsync(userId);
                if (candidatedetail != null)
                {
                    var jobs = await _jobRepository.GetByIdAsync(job.JobId);
                    if (jobs != null)
                    {
                        var recruiter = await _userRepository.GetByIdAsync(jobs.CreatedBy);
                        StringBuilder Rmail = new StringBuilder();
                        Rmail.Append($"<p>JobName:{jobs.Title}</p>");
                        Rmail.Append($"<p>ApplicantName:{candidatedetail.Name}</p>");
                        await _mailservice.SendEmailAsync(recruiter.Email, Rmail, "Recruiter", "", "");

                        StringBuilder AMail = new StringBuilder();
                        AMail.Append($"<p>Applied job Success</p>");
                        AMail.Append($"<p>Applyed job :{jobs.Title}</p>");
                        await _mailservice.SendEmailAsync(candidatedetail.Email, AMail, "Applicant", "", "");
                    }
                }


                return await _candidateRepository.AddAsync(applyjobs);

            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<GetCandidateDto>> GetJobsApplied(int id, PaginationModel pagination)
        {
            var JobApplied = await _jobRepository.GetJobsApplied(id, pagination);
            if (JobApplied != null)
                return JobApplied;
            return null;

        }

        public async Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination)
        {
            var Jobs = await _jobRepository.GetJobsAsync(pagination);
            if (Jobs != null)
                return Jobs;
            return null;

        }
    }

}
