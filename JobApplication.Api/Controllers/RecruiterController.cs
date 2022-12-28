using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using JobApplication.Service.RecruiterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return OkResponse("Success", jobs);
        }


        [HttpPost("GetJobAppliedByCandidate")]
        public async Task<IActionResult> JobAppliedByCandidate(PaginationModel pagination)
        {
            var jobs = await _recruiterService.GetJobAppliedByCandidateAsync(UserId, pagination);
            return OkResponse("Success", jobs);
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob(AddJobDto addJobDto)
        {            
            if (ModelState.IsValid)
            {
                var job = await _recruiterService.AddJobAsync(UserId, addJobDto);
                return OkResponse("Success", job);
            }
            else
            {
                return NotFoundResponse("Unable To Add New Job ", "");
            }
        }
    }
}
