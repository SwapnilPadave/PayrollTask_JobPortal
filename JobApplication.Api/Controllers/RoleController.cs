using JobApplication.Model.Dto.RoleDto;
using JobApplication.Model.Dto.RoleMappingDto;
using JobApplication.Service.RoleMapService;
using JobApplication.Service.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobApplication.Api.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IRoleMappingService _roleMappingService;
        public RoleController(IRoleService roleService, IRoleMappingService roleMappingService)
        {
            _roleService = roleService;
            _roleMappingService = roleMappingService;
        }

        [HttpPost("GetRolById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var result = await _roleService.GetById(id);
            return Ok(result);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> AddRole(AddRoleDto addRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.AddRoleAsync(addRole);
                return Ok(result);
            }
            else
            {
                return BadRequest("Unauthorized Acess.");
            }
            
        }        

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRole(RoleMappingDto roleMapping)
        {
            if (ModelState.IsValid)
            {
                var data = await _roleMappingService.AssignRole(roleMapping);
                return Ok(data);
            }
            else
            {
                return BadRequest("Unauthorized Acess.");
            }            
        }

        [HttpPost("GetRoleMapById/{id}")]
        public async Task<IActionResult> GetRoleMApById(int id)
        {
            var data = await _roleMappingService.GetRoleMappingById(id);
            return Ok(data);
        }

        [HttpPost("GetAllRoleMap")]
        public async Task<IActionResult> GetAllRoleMap()
        {
            var data = await _roleMappingService.GetAllRoleMapping();
            return Ok(data);
        }
    }
}
