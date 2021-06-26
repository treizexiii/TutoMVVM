using System;
using System.Threading.Tasks;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.State.Accounts;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.Commands
{
    class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _viewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;

            try
            {
                string symbol = _viewModel.Symbol;
                int shares = _viewModel.ShareToSell;
                Account acccount = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);
                _accountStore.CurrentAccount = acccount;
                _viewModel.SearchResultSymbol = String.Empty;
                _viewModel.StatusMessage = $"Successfully sold {shares} shares of {symbol}";
            }
            catch (InsufficientShareException ex)
            {
                _viewModel.ErrorMessage = $"Account has insufficient share. You only have {ex.AccountShares} share(s).";
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
