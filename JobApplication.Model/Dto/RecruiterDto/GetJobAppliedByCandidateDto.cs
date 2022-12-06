using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Model.Dto.RecruiterDto
{
    public class GetJobAppliedByCandidateDto
    {
        public int Id { get; set; }
        public string CandidateName { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
