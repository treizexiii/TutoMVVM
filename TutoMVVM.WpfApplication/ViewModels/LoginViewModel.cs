using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;

        public MessageVIewModel ErrorMessageViewModel { get; set; }
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand ViewRegisterCommand { get; set; }

        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);
            ErrorMessageViewModel = new MessageVIewModel();
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
        }
    }
}
