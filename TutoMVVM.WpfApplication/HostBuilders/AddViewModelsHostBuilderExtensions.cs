using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TutoMVVM.Domain.Services;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton(CreateHomeViewModel);
                services.AddSingleton<BuyViewModel>();
                services.AddSingleton<SellViewModel>();
                services.AddSingleton<PortfolioViewModel>();
                services.AddSingleton<AssetSummaryViewModel>();
                services.AddSingleton<MainViewModel>();

                services.AddSingleton<ViewModelDelegateRenavigaor<HomeViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigaor<RegisterViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigaor<LoginViewModel>>();

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services => () => services.GetRequiredService<PortfolioViewModel>());
                services.AddSingleton<CreateViewModel<BuyViewModel>>(services => () => services.GetRequiredService<BuyViewModel>());
                services.AddSingleton<CreateViewModel<SellViewModel>>(services => () => services.GetRequiredService<SellViewModel>());
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
                services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
            });

            return host;
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(
                        MajorIndexListeningViewModel.LoadMajorIndexViewModel(services.GetRequiredService<IMajorIndexService>()),
                        services.GetRequiredService<AssetSummaryViewModel>());
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                        services.GetRequiredService<IAuthenticator>(),
                        services.GetRequiredService<ViewModelDelegateRenavigaor<HomeViewModel>>(),
                        services.GetRequiredService<ViewModelDelegateRenavigaor<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(
                        services.GetRequiredService<ViewModelDelegateRenavigaor<LoginViewModel>>(),
                        services.GetRequiredService<IAuthenticator>(),
                        services.GetRequiredService<ViewModelDelegateRenavigaor<LoginViewModel>>());
        }
    }
}
