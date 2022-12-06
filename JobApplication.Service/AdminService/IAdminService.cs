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
    public interface IAdminService
    {
        Task<IEnumerable<GetUserDto>> GetUsersAsync(PaginationModel pagination);
        Task<IEnumerable<GetUserDto>> GetRecruitersAsync(PaginationModel pagination);
        Task<IEnumerable<GetJobDto>> GetJobsAsync(PaginationModel pagination);
        Task<IEnumerable<GetJobAppliedByCandidateDto>> GetJobAppliedByCandidates(PaginationModel pagination);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> DeleteRecruiterAsync(int id);
        Task<bool> DeleteJobAsync(int id);

    }
}
