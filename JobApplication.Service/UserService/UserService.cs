using JobApplication.Database.Repositories;
using JobApplication.Model.Dto.UserDto;
using JobApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public async Task<IEnumerable<GetUserDto>> GetUsersAsync(PaginationModel pagination)
        {
            try
            {
                var user = await _userRepository.GetUsersAsync(pagination);
                return user;                
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }

        public async Task<UserMaster> GetUserByIdAsync(int Id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(Id);
                return user;
            }
            catch (Exception)
            {

                throw new NullReferenceException();
            }
        }


        public async Task<UserMaster> AddUserAsync(AddUserDto Users)
        {

            try
            {
                UserMaster user = new UserMaster();
                user.Name = Users.Name;
                user.Password = BCrypt.Net.BCrypt.HashPassword(Users.Password);
                user.RoleId = Users.RoleId;
                user.Email = Users.Email;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                user.IsActive = true;
                var result = await _userRepository.AddAsync(user);
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Something Went Wrong..");
            }

        }

        public async Task<UserMaster> UpdateUserAsync(int id, UpdateUserDto Users)
        {
            UserMaster user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.Name = Users.Name;
                user.Email = Users.Email;
                user.ModifiedDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);
                return user;
            }
            return null;
        }

        public async Task<UserMaster> GetUserAsync(string email, string password)
        {

            var user = await _userRepository.GetDefault(x => x.Email == email);
            if (user != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return null;
                }
                return user;
            }
            return null;
        }
    }
}
