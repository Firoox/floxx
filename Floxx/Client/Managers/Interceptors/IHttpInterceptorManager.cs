using System.Threading.Tasks;
using Floxx.Web.Client.Client.Managers;
using Toolbelt.Blazor;

namespace Floxx.Client.Managers.Interceptors
{
    public interface IHttpInterceptorManager : IManager
    {
        void RegisterEvent();
        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);
        void DisposeEvent();
    }
}
