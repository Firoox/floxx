using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Floxx.Client.Extensions;
using Floxx.Core.DTO.Identity;
using Floxx.Shared.Interfaces;

namespace Floxx.Client.Managers.Identity.Users
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;

        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<UserResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Web.Client.Client.Routes.UserEndpoint.GetAll);
            return await response.ToResult<List<UserResponse>>();
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Web.Client.Client.Routes.UserEndpoint.Get(userId));
            return await response.ToResult<UserResponse>();
        }

        public async Task<IResult> RegisterUserAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Web.Client.Client.Routes.UserEndpoint.Register, request);
            return await response.ToResult();
        }

        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Web.Client.Client.Routes.UserEndpoint.ToggleUserStatus, request);
            return await response.ToResult();
        }

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Web.Client.Client.Routes.UserEndpoint.GetUserRoles(userId));
            return await response.ToResult<UserRolesResponse>();
        }

        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(Web.Client.Client.Routes.UserEndpoint.GetUserRoles(request.UserId), request);
            return await response.ToResult<UserRolesResponse>();
        }

        //public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest model)
        //{
        //    var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoint.ForgotPassword, model);
        //    return await response.ToResult();
        //}

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Web.Client.Client.Routes.UserEndpoint.ResetPassword, request);
            return await response.ToResult();
        }
    }
}