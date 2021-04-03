using Floxx.Core.DTO.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Floxx.Client.Pages.Authentication
{
    public partial class Login
    {
        private TokenRequest model = new TokenRequest();

        protected override async Task OnInitializedAsync()
        {
            var state = await stateProvider.GetAuthenticationStateAsync();
            if (state != new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())))
            {
                navigationManager.NavigateTo("/");
            }
        }

        private async Task SubmitAsync()
        {
            var result = await authenticationManager.Login(model);
            if (result.Succeeded)
            {
                snackBar.Add($"{localizer["Welcome"]} {model.Email}.", Severity.Success);
                navigationManager.NavigateTo("/", true);
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    snackBar.Add(localizer[message], Severity.Error);
                }
            }
        }

        private void FillAdminstratorCredentials()
        {
            model.Email = "proelofs@gmail.com";
            model.Password = "Geheim";
        }
    }
}
