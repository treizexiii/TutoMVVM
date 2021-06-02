using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.Commands
{
    class BuyStockCommand : ICommand
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;

        public event EventHandler CanExecuteChanged;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
        }

        public bool CanExecute(object parameter)
        {
            //if (_buyViewModel.SharesToBuy <= 0)
            //{
            //    return false;
            //}
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                Account acccount = await _buyStockService.BuyStock(new Account
                {
                    Id = 3,
                    Balance = 500,
                    AssetTransactions = new List<AssetTransaction>()
                }, _buyViewModel.Symbol, _buyViewModel.SharesToBuy);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
