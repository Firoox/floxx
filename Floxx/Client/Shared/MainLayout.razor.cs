using System;
using System.Threading.Tasks;
using Floxx.Web.Client.Client.Settings;
using MudBlazor;

namespace Floxx.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        MudTheme currentTheme;
        private bool drawerOpen = true;

        private string FirstName { get; set; }
        private string SecondName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }
        private async Task LoadDataAsync()
        {
            var state = await stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            if (user == null) return;
            if (user.Identity.IsAuthenticated)
            {
                this.FirstName = ""; //user.GetFirstName();
                if (this.FirstName.Length > 0)
                {
                    FirstLetterOfName = FirstName[0];
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            currentTheme = await preferenceManager.GetCurrentThemeAsync();
        }
        void Logout()
        {
            string logoutConfirmationText = localizer["Logout Confirmation"];
            string logoutText = localizer["Logout"];
            var parameters = new DialogParameters();
            parameters.Add("ContentText", logoutConfirmationText);
            parameters.Add("ButtonText", logoutText);
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            //dialogService.Show<Dialogs.Logout>("Logout", parameters, options);
        }
        void DrawerToggle()
        {
            drawerOpen = !drawerOpen;
        }
        async Task DarkMode()
        {
            bool isDarkMode = await preferenceManager.ToggleDarkModeAsync();
            if (isDarkMode)
            {
                currentTheme = FloxxTheme.DefaultTheme;
            }
            else
            {
                currentTheme = FloxxTheme.DarkTheme;
            }
        }
        public void Dispose() => interceptor.DisposeEvent();
    }
}
