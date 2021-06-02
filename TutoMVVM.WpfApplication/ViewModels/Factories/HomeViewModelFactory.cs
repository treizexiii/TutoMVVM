namespace TutoMVVM.WpfApplication.ViewModels.Factories
{
    public class HomeViewModelFactory : IViewModelFactory<HomeViewModel>
    {
        private readonly IViewModelFactory<MajorIndexListeningViewModel> _majorIndexViewModelFactory;

        public HomeViewModelFactory(IViewModelFactory<MajorIndexListeningViewModel> majorIndexViewModelFactory)
        {
            _majorIndexViewModelFactory = majorIndexViewModelFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(_majorIndexViewModelFactory.CreateViewModel());
        }
    }
}
