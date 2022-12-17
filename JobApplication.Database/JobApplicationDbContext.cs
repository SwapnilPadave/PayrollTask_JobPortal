using JobApplication.Model;
using JobApplication.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Database
{
    public class JobApplicationDbContext : DbContext
    {
        public JobApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserMaster> User { get; set; }
        public DbSet<RoleMaster> roleMasters { get; set; }
        public DbSet<OtpMaster> otpMasters { get; set; }
        public DbSet<JobMaster> jobMasters { get; set; }
        public DbSet<CandidateMaster> candidateMasters { get; set; }
        public DbSet<RoleMappingModel> roleMappingModels { get; set; }
    }
}
