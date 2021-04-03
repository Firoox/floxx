using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Floxx.Client.Managers.Identity.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Toolbelt.Blazor;

namespace Floxx.Client.Managers.Interceptors
{
    public class HttpInterceptorManager : IHttpInterceptorManager
    {
        private readonly HttpClientInterceptor clientInterceptor;
        private readonly IAuthenticationManager authenticationManager;
        private readonly NavigationManager navigationManager;
        private readonly ISnackbar snackBar;
        public HttpInterceptorManager(HttpClientInterceptor clientInterceptor, IAuthenticationManager authenticationManager, NavigationManager navigationManager, ISnackbar snackBar)
        {
            this.clientInterceptor = clientInterceptor;
            this.authenticationManager = authenticationManager;
            this.navigationManager = navigationManager;
            this.snackBar = snackBar;
        }
        public void RegisterEvent() => clientInterceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("token") && !absPath.Contains("accounts"))
            {
                try
                {
                    var token = await authenticationManager.TryRefreshToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        snackBar.Add("Refreshed Token.", Severity.Success);
                        e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    snackBar.Add("You are Logged Out.", Severity.Error);
                    await authenticationManager.Logout();
                    navigationManager.NavigateTo("/");
                }

            }
        }
        public void DisposeEvent() => clientInterceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
    }
}
