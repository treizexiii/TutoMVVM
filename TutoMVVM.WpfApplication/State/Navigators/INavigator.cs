using System.Windows.Input;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.State.Navigators
{
    public enum ViewType
    {
        Login,
        Home,
        Potfolio,
        Buy,
        Sell
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
