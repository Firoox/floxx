using System.Security.Claims;
using System.Threading.Tasks;
using Floxx.Core.DTO.Identity;
using Floxx.Shared.Interfaces;
using Floxx.Web.Client.Client.Managers;

namespace Floxx.Client.Managers.Identity.Authentication
{
    public interface IAuthenticationManager : IManager
    {
        Task<IResult> Login(TokenRequest model);

        Task<IResult> Logout();
        Task<string> RefreshToken();
        Task<string> TryRefreshToken();

        Task<ClaimsPrincipal> CurrentUser();
    }
}