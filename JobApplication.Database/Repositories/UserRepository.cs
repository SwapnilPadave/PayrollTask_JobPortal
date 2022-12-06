using JobApplication.Database.Infrastructure;
using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Database.Repositories
{
    public interface IUserRepository : IRepository<UserMaster>
    {
        Task<IEnumerable<GetUserDto>> GetUsersAsync(PaginationModel pagination);
    }
    public class UserRepository : Repository<UserMaster>, IUserRepository
    {
        public UserRepository(JobApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<GetUserDto>> GetUsersAsync(PaginationModel pagination)
        {
            var users = await (from u in _context.User
                               select new GetUserDto
                               {
                                   Id = u.Id,
                                   Name = u.Name,
                                   CreatedDate = u.CreatedDate,
                                   Email = u.Email,
                                   IsActive = u.IsActive,

                               })
                                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                 .Take(pagination.PageSize)
                                 .OrderBy(x => x.Id)
                                 .ToListAsync();
            return users;            
        }
    }
}