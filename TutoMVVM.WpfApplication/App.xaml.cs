using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.EntityFramework;
using TutoMVVM.EntityFramework.Services;
using TutoMVVM.FinancialModelingPrepAPI;
using TutoMVVM.FinancialModelingPrepAPI.Services;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            MainWindow window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            string apiKey = ConfigurationManager.AppSettings.Get("financeApiKey");
            services.AddSingleton<FinancialModelingPrepHttpClientFactory>(new FinancialModelingPrepHttpClientFactory(apiKey));

            services.AddSingleton<TutoMVVMDbContextFactory>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();

            services.AddSingleton<IRootViewModelFactory, RootViewModelFactory>();
            services.AddSingleton<IViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
            services.AddSingleton<IViewModelFactory<PortfolioViewModel>, PortfolioViewModelFactory>();
            services.AddSingleton<IViewModelFactory<MajorIndexListeningViewModel>, MajorIndexListeningViewModelFactory>();

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<BuyViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
