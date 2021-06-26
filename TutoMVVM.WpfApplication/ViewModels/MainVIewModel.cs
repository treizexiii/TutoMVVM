using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _ViewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory, IAuthenticator authenticator)
        {
            _ViewModelFactory = viewModelFactory;
            _navigator = navigator;
            _authenticator = authenticator;

            _navigator.StateChanged += Navigator_StateChanged;
            _authenticator.StateChanged += Authenticator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _ViewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
}
