using System.ComponentModel.DataAnnotations;

namespace JobApplication.Model.Dto.JobDto
{
    public class AddJobDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
