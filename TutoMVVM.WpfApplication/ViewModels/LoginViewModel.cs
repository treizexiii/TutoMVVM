using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Authenticator;
using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {
            LoginCommand = new LoginCommand(this, authenticator, renavigator);
        }
    }
}
