using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TutoMVVM.WpfApplication.State.Accounts;
using TutoMVVM.WpfApplication.State.Assets;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<AssetStore>(); ;
            });
            return host;
        }
    }
}
