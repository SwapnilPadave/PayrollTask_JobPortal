using JobApplication.Model.Dto.RecruiterDto;
using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using JobApplication.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplication.Api.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        public readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("GetCandidates")]
        public async Task<IActionResult> GetCandidates(PaginationModel pagination)
        {
            var candidates = await _adminService.GetUsersAsync(pagination);
            return OkResponse("Success", candidates);
        }

        [HttpPost("GetRecruiters")]
        public async Task<IActionResult> GetRecruiters(PaginationModel pagination)
        {
            var recruiters = await _adminService.GetRecruitersAsync(pagination);
            return OkResponse("Success", recruiters);
        }

        [HttpPost("GetJobs")]
        public async Task<IActionResult> GetJobs(PaginationModel pagination)
        {
            var jobs = await _adminService.GetJobsAsync(pagination);
            return OkResponse("Success", jobs);
        }



        [HttpPost("GetJobAppliedByCandidates")]
        public async Task<IActionResult> GetJobAppliedByCandidates(PaginationModel pagination)
        {
            var jobs = await _adminService.GetJobAppliedByCandidates(pagination);
            return OkResponse("Success", jobs);
        }

        [HttpDelete("Deletejob/{Id}")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            var jobs = await _adminService.DeleteJobAsync(Id);
            return OkResponse("Success", jobs);
        }

        [HttpDelete("DeleteRecruiter/{Id}")]
        public async Task<IActionResult> DeleteRecruiter(int Id)
        {
            var recruiter = await _adminService.DeleteRecruiterAsync(Id);
            return OkResponse("Success", recruiter);
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = await _adminService.DeleteUserAsync(Id);
            return OkResponse("Success", user);
        }


        [HttpPost("ExportToExcel")]
        public async Task<IActionResult> ExportFile(PaginationModel pagination)
        {
            IEnumerable<GetUserDto> candidatesList = await _adminService.GetUsersAsync(pagination);
            IEnumerable<GetUserDto> recruitersList = await _adminService.GetRecruitersAsync(pagination);
            IEnumerable<GetJobAppliedByCandidateDto> jobsAppliedByCandidatesList = await _adminService.GetJobAppliedByCandidates(pagination);

            List<IEnumerable<dynamic>> data = new List<IEnumerable<dynamic>>();
            data.Add(candidatesList);
            data.Add(recruitersList);
            data.Add(jobsAppliedByCandidatesList);
            return Export(data);
        }

    }
}

