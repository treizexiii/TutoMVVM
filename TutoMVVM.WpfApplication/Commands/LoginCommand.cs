using System;
using System.Windows.Controls;
using System.Windows.Input;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly INavigator _navigator;
        private readonly LoginViewModel _loginViewModel;

        public event EventHandler CanExecuteChanged;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            bool success = await _authenticator.Login(_loginViewModel.Username, parameter.ToString());

            if (success)
            {
                _renavigator.Renavigate();
            }
        }
    }
}
