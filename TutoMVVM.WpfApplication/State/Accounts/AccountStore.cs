using System;
using TutoMVVM.Domain.Models;

namespace TutoMVVM.WpfApplication.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        private Account _currentAccount;

        public Account CurrentAccount
        {
            get { return _currentAccount; }
            set { _currentAccount = value; StateChanged?.Invoke(); }
        }

        public event Action StateChanged;
    }
}
