using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _loginViewModel = loginViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;

            try
            {
                await _authenticator.Login(_loginViewModel.Username, parameter.ToString());

                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Incorrect password";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed";
            }
        }
    }
}
