using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _email;
        private string _username;
        private string _password;
        private string _confirmPassword;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }

        public MessageVIewModel ErrorMessageViewModel { get; }
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }

        public RegisterViewModel(IRenavigator loginRenavigator, IAuthenticator authenticator, IRenavigator registerRenavigation)
        {
            ErrorMessageViewModel = new MessageVIewModel();
            RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigation);
            ViewLoginCommand = new RenavigateCommand(loginRenavigator);
        }
    }
}
