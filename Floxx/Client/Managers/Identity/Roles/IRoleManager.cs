using System.Collections.Generic;
using System.Threading.Tasks;
using Floxx.Core.DTO.Identity;
using Floxx.Shared.Interfaces;
using Floxx.Web.Client.Client.Managers;

namespace Floxx.Client.Managers.Identity.Roles
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}