using TutoMVVM.Domain.Services;

namespace TutoMVVM.WpfApplication.ViewModels.Factories
{
    class MajorIndexListeningViewModelFactory : IViewModelFactory<MajorIndexListeningViewModel>
    {
        private readonly IMajorIndexService _majorIndexService;

        public MajorIndexListeningViewModelFactory(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public MajorIndexListeningViewModel CreateViewModel()
        {
            return MajorIndexListeningViewModel.LoadMajorIndexViewModel(_majorIndexService);
        }
    }
}
