using System.ComponentModel.DataAnnotations;

namespace JobApplication.Model.Dto.JobDto
{
    public class JobApplyDto
    {
        [Required]
        public int JobId { get; set; }
    }
}
