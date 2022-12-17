using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Model.Models
{
    [Table("Candidate")]
    public class CandidateMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int AppliedJobId { get; set; }

        [Required]
        public DateTime AppliedAt { get; set; }
    }
}
