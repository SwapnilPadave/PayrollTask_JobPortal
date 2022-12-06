﻿using JobApplication.Model.Dto.JobDto;
using JobApplication.Model.Models;
using JobApplication.Service.JobService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Api.Controllers
{
    [Authorize(Policy = "AdminAndCandidate")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : BaseController
    {
        private readonly IJobService _jobservice;
        public CandidateController(IJobService jobService)
        {
            _jobservice = jobService;
        }

        [HttpPost("GetJobs")]
        public async Task<IActionResult> GetJobs(PaginationModel pagination)
        {
            var jobs = await _jobservice.GetJobsAsync(pagination);
            if (jobs != null)
            {
                return OkResponse("Success", jobs);
            }
            return BadResponse("Unable to get List of Jobs", "");
        }

        [HttpPost("GetAppliedJobsByCandidate")]
        public async Task<IActionResult> GetAppliedJobs(PaginationModel pagination)
        {
            var jobs = await _jobservice.GetJobsApplied(UserId, pagination);
            if (jobs != null)
            {
                return OkResponse("Success", jobs);
            }
            return BadResponse("Unable to get List of Jobs", "");
        }

        [HttpPost("GetApplyJob")]
        public async Task<IActionResult> GetApplyJob(JobApplyDto job)
        {
            var jobs = await _jobservice.ApplyJobsAsync(UserId, job);
            if (jobs != null)
            {
                return OkResponse("Job applied Success", jobs);
            }
            return BadResponse("Unable to apply Job", "");
        }


    }
}
