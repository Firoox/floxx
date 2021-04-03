using System.Threading.Tasks;
using Floxx.Core.DTO.Identity;
using Floxx.Core.Interfaces;
using Floxx.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Floxx.Server.Controllers
{
    [Route("api/identity/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [Authorize(Policy = Permissions.Roles.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await roleService.GetAllAsync();
            return Ok(roles);
        }

        [Authorize(Policy = Permissions.Roles.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(RoleRequest request)
        {
            var response = await roleService.SaveAsync(request);
            return Ok(response);
        }

        [Authorize(Policy = Permissions.Roles.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await roleService.DeleteAsync(id);
            return Ok(response);
        }

        [Authorize(Policy = Permissions.Roles.Edit)]
        [HttpGet("permissions/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId([FromRoute] string roleId)
        {
            var response = await roleService.GetAllPermissionsAsync(roleId);
            return Ok(response);
        }

        [Authorize(Policy = Permissions.Roles.Edit)]
        [HttpPut("permissions/update")]
        public async Task<IActionResult> Update(PermissionRequest model)
        {
            var response = await roleService.UpdatePermissionsAsync(model);
            return Ok(response);
        }
    }
}
