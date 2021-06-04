using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services.AuthenticationServices;
using TutoMVVM.WpfApplication.Models;

namespace TutoMVVM.WpfApplication.State.Authenticator
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private Account _currentAccount;

        public Account CurrentAccount
        {
            get { return _currentAccount; }
            private set { _currentAccount = value; OnPropertyChanged(nameof(CurrentAccount)); OnPropertyChanged(nameof(IsLoggedIn)); }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<bool> Login(string username, string password)
        {
            bool success = true;
            try
            {
                CurrentAccount = await _authenticationService.Login(username, password);
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public async void Logout()
        {
            CurrentAccount = null;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}
