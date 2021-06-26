using System.Windows.Input;
using TutoMVVM.Domain.Services;
using TutoMVVM.Domain.Services.TransationServices;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.State.Accounts;
using TutoMVVM.WpfApplication.State.Assets;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        private AssetViewModel _selectedAsset;
        private string _searchResultSymbol = string.Empty;
        private double _stockPrice;
        private int _sharesToSell;

        public AssetViewModel SelectedAsset
        {
            get { return _selectedAsset; }
            set { _selectedAsset = value; OnPropertyChanged(nameof(SelectedAsset)); }
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

        public int ShareToSell
        {
            get { return _sharesToSell; }
            set { _sharesToSell = value; OnPropertyChanged(nameof(ShareToSell)); OnPropertyChanged(nameof(TotalPrice)); }
        }

        public string Symbol => SelectedAsset?.Symbol;
        public double TotalPrice => ShareToSell * StockPrice;

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }

        public AssetListingViewModel AssetListingViewModel { get; }
        public MessageVIewModel ErrorMessageViewModel { get; set; }
        public string ErrorMessage { set => ErrorMessageViewModel.Message = value; }
        public MessageVIewModel StatusMessageViewModel { get; set; }
        public string StatusMessage { set => StatusMessageViewModel.Message = value; }

        public SellViewModel(AssetStore assetStore,
            IStockPriceService stockPriceService,
            ISellStockService sellStockService,
            IAccountStore accountStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);

            ErrorMessageViewModel = new MessageVIewModel();
            StatusMessageViewModel = new MessageVIewModel();
        }
    }
}
