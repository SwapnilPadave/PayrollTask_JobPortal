using JobApplication.Model.Dto.LoginDto;
using JobApplication.Model.Models;
using JobApplication.Service.AccountService;
using JobApplication.Service.RoleService;
using JobApplication.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Http;

namespace JobApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        public AccountController(IUserService userService, IRoleService roleService, IConfiguration configuration, IAccountService accountService)
        {
            _userService = userService;
            _roleService = roleService;
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _userService.GetUserAsync(loginDto.Email, loginDto.Password);
            if (user != null)
            {
                var role = await _roleService.GetById(user.RoleId);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("UserId", Convert.ToString(user.Id), ClaimValueTypes.Integer),
                    new Claim("Email", user.Email, ClaimValueTypes.String),
                    new Claim("RoleId", Convert.ToString(user.RoleId), ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Role, role.Role, ClaimValueTypes.String),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new ResponseModel { StatusCode = StatusCodes.Status401Unauthorized, Message = "Invalid Email or password" });
        }


        [HttpPost]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _accountService.ForgotPassword(email);
            if (user == true)
            {
                return OkResponse("Otp sent to the register email address..", user);
            }
            return NotFoundResponse("Incorrect Details..", "");
        }


        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(int otp, string newPassword, string confirmPassword)
        {
            var user = await _accountService.ResetPassword(otp, newPassword, confirmPassword);
            if (user != null)
            {
                return OkResponse("Password updated successfully..", user);
            }
            return BadResponse("Incorrect Details..", "");
        }
    }
}

