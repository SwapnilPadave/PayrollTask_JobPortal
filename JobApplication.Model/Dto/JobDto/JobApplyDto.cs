using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobApplication.Model.Dto.JobDto
{
    public class JobApplyDto
    {
        [Required]
        public int JobId { get; set; }
    }
}
