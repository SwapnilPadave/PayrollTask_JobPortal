using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplication.Model.Models
{
    [Table("RoleMappingModel")]
    public class RoleMappingModel
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
