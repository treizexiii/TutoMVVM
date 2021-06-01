using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewtType)
            {
                ViewtType viewtType = (ViewtType)parameter;

                switch (viewtType)
                {
                    case ViewtType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewtType.Potfolio:
                        _navigator.CurrentViewModel = new PortfolioViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
