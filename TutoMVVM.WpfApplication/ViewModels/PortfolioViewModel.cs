using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TutoMVVM.WpfApplication.State.Assets;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
        }
    }
}
