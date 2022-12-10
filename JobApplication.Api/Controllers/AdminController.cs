using JobApplication.Model.Dto.RecruiterDto;
using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using JobApplication.Service.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (candidates != null)
            {
                return OkResponse("Success", candidates);
            }
            return BadResponse("Unable to get List of Candidates", "");
        }

        [HttpPost("GetRecruiters")]
        public async Task<IActionResult> GetRecruiters(PaginationModel pagination)
        {
            var recruiters = await _adminService.GetRecruitersAsync(pagination);
            if (recruiters != null)
            {
                return OkResponse("Success", recruiters);
            }
            return BadResponse("Unable to get List of Recruiters", "");
        }

        [HttpPost("GetJobs")]
        public async Task<IActionResult> GetJobs(PaginationModel pagination)
        {
            var jobs = await _adminService.GetJobsAsync(pagination);
            if (jobs != null)
            {
                return OkResponse("Success", jobs);
            }
            return BadResponse("Unable to get List of Jobs", "");
        }



        [HttpPost("GetJobAppliedByCandidates")]
        public async Task<IActionResult> GetJobAppliedByCandidates(PaginationModel pagination)
        {
            var jobs = await _adminService.GetJobAppliedByCandidates(pagination);
            if (jobs != null)
            {
                return OkResponse("Success", jobs);
            }
            return BadResponse("Unable to get List of Jobs", "");
        }

        [HttpDelete("Deletejob/{Id}")]
        public async Task<IActionResult> DeleteJob(int Id)
        {
            var jobs = await _adminService.DeleteJobAsync(Id);
            if (jobs)
            {
                return OkResponse("Success", jobs);
            }
            else
            {
                return BadResponse("Unable to delete job", "");
            }
        }

        [HttpDelete("DeleteRecruiter/{Id}")]
        public async Task<IActionResult> DeleteRecruiter(int Id)
        {
            var recruiter = await _adminService.DeleteRecruiterAsync(Id);
            if (recruiter)
            {
                return OkResponse("Success", recruiter);
            }
            else
            {
                return BadResponse("Unable to delete Recruiter", "");
            }
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = await _adminService.DeleteUserAsync(Id);
            if (user)
            {
                return OkResponse("Success", user);
            }
            else
            {
                return BadResponse("Unable to delete User", "");
            }
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

