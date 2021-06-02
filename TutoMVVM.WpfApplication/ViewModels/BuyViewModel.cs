using System.Windows.Input;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.Commands;

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
            set { _symbol = value; OnPropertyChange(nameof(Symbol)); }
        }

        public string SearchResultSymbol
        {
            get { return _searchResultSymbol; }
            set { _searchResultSymbol = value; OnPropertyChange(nameof(SearchResultSymbol)); OnPropertyChange(nameof(TotalPrice)); }
        }

        public double StockPrice
        {
            get { return _stockPrice; }
            set { _stockPrice = value; OnPropertyChange(nameof(StockPrice)); OnPropertyChange(nameof(TotalPrice)); }
        }

        public int SharesToBuy
        {
            get { return _sharesToBuy; }
            set { _sharesToBuy = value; OnPropertyChange(nameof(SharesToBuy)); OnPropertyChange(nameof(TotalPrice)); }
        }

        public double TotalPrice { get { return SharesToBuy * StockPrice; } }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService);
        }

    }
}
