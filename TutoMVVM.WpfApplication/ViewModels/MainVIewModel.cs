using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRootViewModelFactory _rootViewModelFactory;

        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IRootViewModelFactory rootViewModelFactory, IAuthenticator authenticator)
        {
            _rootViewModelFactory = rootViewModelFactory;
            Navigator = navigator;
            Authenticator = authenticator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, rootViewModelFactory) ;
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
