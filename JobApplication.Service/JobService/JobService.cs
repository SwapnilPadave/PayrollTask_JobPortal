using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.CandidateDto;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using JobApplication.Service.EmailService;
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
            try
            {
                var applyJobs = new CandidateMaster();
                applyJobs.CandidateId = userId;
                applyJobs.AppliedJobId = job.JobId;
                applyJobs.AppliedAt = DateTime.Now;
                if (job != null)
                {
                    var candidateDetail = await _userRepository.GetByIdAsync(userId);
                    if (candidateDetail != null)
                    {
                        var jobs = await _jobRepository.GetByIdAsync(job.JobId);
                        if (jobs != null)
                        {
                            var recruiter = await _userRepository.GetByIdAsync(jobs.CreatedBy);
                            StringBuilder rMail = new StringBuilder();
                            rMail.Append($"<p>JobName:{jobs.Title}</p>");
                            rMail.Append($"<p>ApplicantName:{candidateDetail.Name}</p>");
                            await _mailservice.SendEmailAsync(recruiter.Email, rMail, "Recruiter", "", "");

                            StringBuilder aMail = new StringBuilder();
                            aMail.Append($"<p>Applied job Success</p>");
                            aMail.Append($"<p>Applyed job :{jobs.Title}</p>");
                            await _mailservice.SendEmailAsync(candidateDetail.Email, aMail, "Applicant", "", "");
                        }
                    }
                    return await _candidateRepository.AddAsync(applyJobs);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<GetCandidateDto>> GetJobsApplied(int id, PaginationModel pagination)
        {
            var jobApplied = await _jobRepository.GetJobsApplied(id, pagination);
            if (jobApplied != null)
                return jobApplied;
            return null;
        }

        public async Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination)
        {
            var jobs = await _jobRepository.GetJobsAsync(pagination);
            if (jobs != null)
                return jobs;
            return null;
        }
    }

}
