using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.Models;
using TutoMVVM.WpfApplication.ViewModels;

namespace TutoMVVM.WpfApplication.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChange(nameof(CurrentViewModel));
            }
        }

        public ICommand updateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);

    }
}
