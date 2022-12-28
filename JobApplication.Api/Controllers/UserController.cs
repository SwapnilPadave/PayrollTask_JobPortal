using JobApplication.Model.Dto.UserDto;
using JobApplication.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobApplication.Api.Controllers
{
    [Authorize(Policy = "AdminAndCandidate")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService user)
        {
            _userService = user;
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> UserRegistrationAsync(AddUserDto addUserDto)
        {            
            if (ModelState.IsValid)
            {
                var user = await _userService.AddUserAsync(addUserDto);
                return OkResponse("User Registration Successfully", user);
            }                           
            else
            {
                return BadResponse("User Registration Failed", "Contact admin.");
            }            
        }

        [HttpGet("Getuser/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return OkResponse("Success", user);
        }

        [HttpPost("Updateuser/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserDto updateUserDto)
        {            
            if (ModelState.IsValid)
            {
                var user = await _userService.UpdateUserAsync(id, updateUserDto);
                return OkResponse("Updated Sucessfully", user);
            }
            else
            {
                return BadResponse("Failed To Update", "");
            }
        }
    }
}
