using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Floxx.Client.Extensions;
using Floxx.Core.DTO.Identity;
using Floxx.Shared.Interfaces;

namespace Floxx.Client.Managers.Identity.Roles
{
    public class RoleManager : IRoleManager
    {
        private readonly HttpClient _httpClient;

        public RoleManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Web.Client.Client.Routes.RolesEndpoint.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<List<RoleResponse>>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync(Web.Client.Client.Routes.RolesEndpoint.GetAll);
            return await response.ToResult<List<RoleResponse>>();
        }

        public async Task<IResult<string>> SaveAsync(RoleRequest role)
        {
            var response = await _httpClient.PostAsJsonAsync(Web.Client.Client.Routes.RolesEndpoint.Save, role);
            return await response.ToResult<string>();
        }

        public async Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId)
        {
            var response = await _httpClient.GetAsync(Web.Client.Client.Routes.RolesEndpoint.GetPermissions + roleId);
            return await response.ToResult<PermissionResponse>();
        }

        public async Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(Web.Client.Client.Routes.RolesEndpoint.UpdatePermissions, request);
            return await response.ToResult<string>();
        }
    }
}