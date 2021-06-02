using TutoMVVM.Domain.Models;
using TutoMVVM.Domain.Services;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class MajorIndexListeningViewModel : ViewModelBase
    {
        private IMajorIndexService _majorIndexService;
        private MajorIndex _dowJones;
        private MajorIndex _nasdaq;
        private MajorIndex _sp500;

        public MajorIndex DowJones
        {
            get { return _dowJones; }
            set { _dowJones = value; OnPropertyChange(nameof(DowJones)); }
        }

        public MajorIndex Nasdaq
        {
            get { return _nasdaq; }
            set { _nasdaq = value; OnPropertyChange(nameof(Nasdaq)); }
        }

        public MajorIndex SP500
        {
            get { return _sp500; }
            set { _sp500 = value; OnPropertyChange(nameof(SP500)); }
        }


        public MajorIndexListeningViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexListeningViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListeningViewModel majorIndexViewModel = new MajorIndexListeningViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }

        private void LoadMajorIndexes()
        {
            _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    DowJones = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Nasdaq = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    SP500 = task.Result;
                }
            });
        }
    }
}
