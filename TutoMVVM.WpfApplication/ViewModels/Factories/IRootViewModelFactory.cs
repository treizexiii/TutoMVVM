using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels.Factories
{
    public interface IRootViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewtType);
    }
}
