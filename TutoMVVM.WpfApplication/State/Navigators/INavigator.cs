using System.Windows.Input;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.State.Navigators
{
    public enum ViewType
    {
        Home,
        Potfolio,
        Buy,
        Sell
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand updateCurrentViewModelCommand { get; }
    }
}
