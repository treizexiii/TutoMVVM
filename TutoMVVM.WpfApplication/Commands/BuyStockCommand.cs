using System;
using System.Windows;
using System.Windows.Input;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.State.Accounts;
using TutoMVVM.WpfApplication.ViewModels;
using System.Threading.Tasks;

namespace TutoMVVM.WpfApplication.Commands
{
    class BuyStockCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _buyViewModel.ErrorMessage = string.Empty;
            _buyViewModel.StatusMessage = string.Empty;

            try
            {
                string symbol = _buyViewModel.Symbol;
                int shares = _buyViewModel.SharesToBuy;
                Account acccount = await _buyStockService.BuyStock(_accountStore.CurrentAccount, symbol, shares);
                _accountStore.CurrentAccount = acccount;
                _buyViewModel.StatusMessage = $"Successfully purchased {shares} shares of {symbol}";
            }
            catch (InsufficientFundsException)
            {
                _buyViewModel.ErrorMessage = "Account has insufficient funds. Please transfer more money into your account.";
            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
