using System.Threading.Tasks;
using Blazored.LocalStorage;
using Floxx.Web.Client.Client.Settings;
using MudBlazor;

namespace Floxx.Client.Managers.Preferences
{
    public class PreferenceManager : IPreferenceManager
    {
        private readonly ILocalStorageService localStorageService;

        public PreferenceManager(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            Preference preference = await GetPreference();

            preference.IsDarkMode = !preference.IsDarkMode;

            await SetPreference(preference);

            return !preference.IsDarkMode;
        }

        public async Task ChangeLanguageAsync(string languageCode)
        {
            var preference = await GetPreference();
            preference.LanguageCode = languageCode;
            await SetPreference(preference);
        }

        public async Task<MudTheme> GetCurrentThemeAsync()
        {
            var preference = await GetPreference();
            if (preference.IsDarkMode) return FloxxTheme.DarkTheme;
            return FloxxTheme.DefaultTheme;
        }

        public async Task<Preference> GetPreference()
        {
            return await localStorageService.GetItemAsync<Preference>("preference") ?? new Preference();
        }

        public async Task SetPreference(Preference preference)
        {
            await localStorageService.SetItemAsync("preference", preference);
        }
    }
}
