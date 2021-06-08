using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.AuthenticationServices;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.EntityFramework;
using TutoMVVM.EntityFramework.Services;
using TutoMVVM.FinancialModelingPrepAPI;
using TutoMVVM.FinancialModelingPrepAPI.Services;
using TutoMVVM.WpfApplication.State.Accounts;
using TutoMVVM.WpfApplication.State.Assets;
using TutoMVVM.WpfApplication.State.Authenticator;
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
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddSingleton<BuyViewModel>();
            services.AddSingleton<PortfolioViewModel>();
            services.AddSingleton<AssetSummaryViewModel>();
            services.AddSingleton<HomeViewModel>(services => new HomeViewModel(
                    MajorIndexListeningViewModel.LoadMajorIndexViewModel(
                        services.GetRequiredService<IMajorIndexService>()),
                    services.GetRequiredService<AssetSummaryViewModel>()));

            services.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
            {
                return () => services.GetRequiredService<HomeViewModel>();
            });

            services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
            {
                return () => services.GetRequiredService<PortfolioViewModel>();
            });
            services.AddSingleton<CreateViewModel<BuyViewModel>>(services =>
            {
                return () => services.GetRequiredService<BuyViewModel>();
            });
            services.AddSingleton<ViewModelDelegateRenavigaor<HomeViewModel>>();
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => new LoginViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<ViewModelDelegateRenavigaor<HomeViewModel>>());
            });

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddSingleton<AssetStore>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<BuyViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
