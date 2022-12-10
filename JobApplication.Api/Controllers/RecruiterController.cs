using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using JobApplication.Service.RecruiterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Api.Controllers
{
    [Authorize(Policy = "AdminAndRecruiter")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : BaseController
    {
        public readonly IRecruiterService _recruiterService;
        public RecruiterController(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        [HttpPost("GetPostedJob")]
        public async Task<IActionResult> GetPostedJob(PaginationModel pagination)
        {
            var jobs = await _recruiterService.GetPostedJobAsync(UserId, pagination);
            if (jobs != null)
            {
                return OkResponse("Success", jobs);
            }
            else
            {
                return NotFoundResponse("Jobs Not Found", "");
            }
        }


        [HttpPost("GetJobAppliedByCandidate")]
        public async Task<IActionResult> JobAppliedByCandidate(PaginationModel pagination)
        {
            var jobs = await _recruiterService.GetJobAppliedByCandidateAsync(UserId, pagination);
            if (jobs != null)
            {
                return OkResponse("Success", jobs);
            }
            else
            {
                return NotFoundResponse("Jobs Not Found", "");
            }
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob(AddJobDto addJobDto)
        {
            var job = await _recruiterService.AddJobAsync(UserId, addJobDto);
            if (job != null)
            {
                return OkResponse("Success", job);
            }
            else
            {
                return NotFoundResponse("Unable TO Add New Job ", "");
            }
        }
    }
}
