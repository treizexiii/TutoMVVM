using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    string apiKey = context.Configuration.GetValue<string>("financeApiKey");

                    services.AddSingleton<FinancialModelingPrepHttpClientFactory>(new FinancialModelingPrepHttpClientFactory(apiKey));

                    string connectionString = context.Configuration.GetConnectionString("default");
                    services.AddDbContext<TutoMVVMDbContext>(o => o.UseSqlServer(connectionString));
                    services.AddSingleton<TutoMVVMDbContextFactory>(new TutoMVVMDbContextFactory(connectionString));
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
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            MainWindow window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
