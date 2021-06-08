using System;
using System.Windows;
using System.Windows.Input;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.State.Accounts;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.Commands
{
    class BuyStockCommand : ICommand
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public event EventHandler CanExecuteChanged;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                Account acccount = await _buyStockService.BuyStock(_accountStore.CurrentAccount, _buyViewModel.Symbol, _buyViewModel.SharesToBuy);
                _accountStore.CurrentAccount = acccount;
                MessageBox.Show("Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
