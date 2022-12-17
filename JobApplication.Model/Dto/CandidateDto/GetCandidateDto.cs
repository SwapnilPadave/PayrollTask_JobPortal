using System;

namespace JobApplication.Model.Dto.CandidateDto
{
    public class GetCandidateDto
    {
        public int Id { get; set; }

        public string CandidateName { get; set; }

        public string JobTitle { get; set; }

        public string JobDescription { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}
