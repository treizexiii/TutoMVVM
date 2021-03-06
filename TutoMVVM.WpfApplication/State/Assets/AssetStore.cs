using System;
using System.Collections.Generic;
using TutoMVVM.Domain.Models;
using TutoMVVM.WpfApplication.State.Accounts;

namespace TutoMVVM.WpfApplication.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;

        public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        public IEnumerable<AssetTransaction> AssetTransactions => _accountStore.CurrentAccount?.AssetTransactions ?? new List<AssetTransaction>();

        public event Action StateChanged;

        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}
