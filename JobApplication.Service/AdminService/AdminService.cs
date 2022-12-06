﻿using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Dto.RecruiterDto;
using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.AdminService
{
    public class AdminService : IAdminService
    {

        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        public AdminService(IJobRepository jobRepository, IUserRepository userRepository, IAdminRepository adminRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _adminRepository = adminRepository;

        }
        public async Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination)
        {
            var Jobs = await _adminRepository.GetJobsAsync(pagination);
            if (Jobs != null)
                return Jobs;
            return null;

        }

        public async Task<IEnumerable<GetUserDto>> GetRecruitersAsync(PaginationModel pagination)
        {
            var recruiters = await _adminRepository.GetRecruitersAsync(pagination);
            if (recruiters != null)
                return recruiters;
            return null;
        }

        public async Task<IEnumerable<GetUserDto>> GetUsersAsync(PaginationModel pagination)
        {
            var users = await _adminRepository.GetUsersAsync(pagination);
            if (users != null)
                return users;
            return null;

        }

        public async Task<IEnumerable<GetJobAppliedByCandidateDto>> GetJobAppliedByCandidates(PaginationModel pagination)
        {
            var AppliedJobs = await _adminRepository.GetJobAppliedByCandidates(pagination);
            if (AppliedJobs != null)
                return AppliedJobs;
            return null;

        }


        public async Task<bool> DeleteJobAsync(int id)
        {
            JobMaster job = await _jobRepository.GetByIdAsync(id);
            job.isActive = false;
            if (_jobRepository.UpdateAsync(job).IsCompleted)
            {
                return true;
            }
            {
                return false;
            }

        }

        public async Task<bool> DeleteRecruiterAsync(int id)
        {
            UserMaster user = await _userRepository.GetByIdAsync(id);
            user.IsActive = false;
            if (_userRepository.UpdateAsync(user).IsCompleted)
            {
                return true;
            }
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            UserMaster user = await _userRepository.GetByIdAsync(id);
            user.IsActive = false;
            if (_userRepository.UpdateAsync(user).IsCompleted)
            {
                return true;
            }
            {
                return false;
            }
        }
    }
}