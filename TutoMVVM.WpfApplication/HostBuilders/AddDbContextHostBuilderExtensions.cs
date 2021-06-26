using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TutoMVVM.EntityFramework;

namespace TutoMVVM.WpfApplication.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("sqlite");
                Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);
                services.AddDbContext<TutoMVVMDbContext>(configureDbContext);
                services.AddSingleton<TutoMVVMDbContextFactory>(new TutoMVVMDbContextFactory(configureDbContext));
            });

            return host;
        }
    }
}
