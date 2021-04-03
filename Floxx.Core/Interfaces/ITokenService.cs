using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floxx.Core.DTO.Identity;
using Floxx.Shared.Wrapper;

namespace Floxx.Core.Interfaces
{
    public interface ITokenService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);
        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}
