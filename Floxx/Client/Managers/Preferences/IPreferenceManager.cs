using System.Threading.Tasks;
using Floxx.Web.Client.Client.Settings;
using MudBlazor;

namespace Floxx.Client.Managers.Preferences
{
    public interface IPreferenceManager
    {
        Task SetPreference(Preference preference);

        Task<Preference> GetPreference();

        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();

        Task ChangeLanguageAsync(string languageCode);
    }
}
