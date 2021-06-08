namespace TutoMVVM.WpfApplication.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MajorIndexListeningViewModel MajorIndexViewModel { get; }
        public AssetSummaryViewModel AssetSummaryViewModel { get; }

        public HomeViewModel(MajorIndexListeningViewModel majorIndexViewModel, AssetSummaryViewModel assetSummaryViewModel)
        {
            MajorIndexViewModel = majorIndexViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

    }
}
