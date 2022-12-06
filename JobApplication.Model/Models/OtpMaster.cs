using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobApplication.Model
{
    [Table("Otp")]
    public class OtpMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Otp { get; set; }
        [Required]
        public DateTime expiry { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int GenerateBy { get; set; }
    }
}
