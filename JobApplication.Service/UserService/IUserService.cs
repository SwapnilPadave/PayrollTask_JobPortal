using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobApplication.Service.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDto>> GetUsersAsync(PaginationModel pagination);
        Task<UserMaster> GetUserByIdAsync(int id);
        Task<UserMaster> AddUserAsync(AddUserDto Users);
        Task<UserMaster> UpdateUserAsync(int id, UpdateUserDto Users);
        Task<UserMaster> GetUserAsync(string email, string password);
    }
}
