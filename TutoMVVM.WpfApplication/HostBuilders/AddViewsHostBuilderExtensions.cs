using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
            });

            return host;
        }
    }
}
