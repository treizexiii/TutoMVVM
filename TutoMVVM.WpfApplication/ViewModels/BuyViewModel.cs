using System.Windows.Input;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Accounts;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        private string _symbol;
        private string _searchResultSymbol = string.Empty;
        private double _stockPrice;
        private int _sharesToBuy;

        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; OnPropertyChanged(nameof(Symbol)); }
        }

        public string SearchResultSymbol
        {
            get { return _searchResultSymbol; }
            set { _searchResultSymbol = value; OnPropertyChanged(nameof(SearchResultSymbol)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        public double StockPrice
        {
            get { return _stockPrice; }
            set { _stockPrice = value; OnPropertyChanged(nameof(StockPrice)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        public int SharesToBuy
        {
            get { return _sharesToBuy; }
            set { _sharesToBuy = value; OnPropertyChanged(nameof(SharesToBuy)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        public double TotalPrice { get { return SharesToBuy * StockPrice; } }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
        }

    }
}
