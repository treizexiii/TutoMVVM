using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TutoMVVM.Domain.Exceptions;
using TutoMVVM.Domain.Services;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.Commands
{
    class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private readonly IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_buyViewModel.Symbol);
                _buyViewModel.SearchResultSymbol = _buyViewModel.Symbol.ToUpper();
                _buyViewModel.StockPrice = stockPrice;
            }
            catch(InvalidSymbolException)
            {
                _buyViewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _buyViewModel.ErrorMessage = "Failed to get the symbol's information.";
            }
        }
    }
}
