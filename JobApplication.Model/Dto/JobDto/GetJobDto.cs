using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Model.Dto.JobDto
{
    public class GetJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
