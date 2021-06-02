namespace TutoMVVM.WpfApplication.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MajorIndexListeningViewModel MajorIndexViewModel { get; set; }

        public HomeViewModel(MajorIndexListeningViewModel majorIndexViewModel)
        {
            MajorIndexViewModel = majorIndexViewModel;
        }

    }
}
