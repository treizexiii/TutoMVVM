using TutoMVVM.WpfApplication.ViewModels;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.State.Navigators
{
    public class ViewModelFactoryRenavigaor<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory<TViewModel> _viewModelFactory;

        public ViewModelFactoryRenavigaor(INavigator navigator, IViewModelFactory<TViewModel> viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel();
        }
    }
}
