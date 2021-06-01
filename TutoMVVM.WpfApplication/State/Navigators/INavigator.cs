using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.State.Navigators
{
    public enum ViewtType
    {
        Home,
        Potfolio
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand updateCurrentViewModelCommand { get; }
    }
}
