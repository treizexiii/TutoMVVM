using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services.AuthenticationServices;
using TutoMVVM.WpfApplication.State.Accounts;

namespace TutoMVVM.WpfApplication.State.Authenticator
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private IAccountStore _accountStore;

        public Account CurrentAccount
        {
            get { return _accountStore.CurrentAccount; }
            private set { _accountStore.CurrentAccount = value; StateChanged?.Invoke(); }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action StateChanged;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore currentAccount)
        {
            _authenticationService = authenticationService;
            _accountStore = currentAccount;
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

        public void Logout()
        {
            CurrentAccount = null;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}
