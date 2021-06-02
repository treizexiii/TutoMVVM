using System;
using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly IViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;
        private readonly BuyViewModel _buyViewModel;

        public RootViewModelFactory(IViewModelFactory<HomeViewModel> homeViewModelFactory, 
            IViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            BuyViewModel buyViewModel)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _buyViewModel = buyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _homeViewModelFactory.CreateViewModel();
                case ViewType.Potfolio:
                    return _portfolioViewModelFactory.CreateViewModel();
                case ViewType.Buy:
                    return _buyViewModel;
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");
            }
        }
    }
}
