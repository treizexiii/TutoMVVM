using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TutoMVVM.FinancialModelingPrepAPI;
using TutoMVVM.FinancialModelingPrepAPI.Model;

namespace TutoMVVM.WpfApplication.HostBuilders
{
    public static class AddApiHostBuilderExtensions
    {
        public static IHostBuilder AddApi(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton(new FinancialModelingPrepApiKey(context.Configuration.GetValue<string>("financeApiKey")));
                services.AddHttpClient<FinancialModelingPrepHttpClient>(c => 
                {
                    c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
                });
            });

            return host;
        }
    }
}
