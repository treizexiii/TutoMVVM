using System.Windows.Input;
using TutoMVVM.WpfApplication.Commands;
using TutoMVVM.WpfApplication.Models;
using TutoMVVM.WpfApplication.ViewModels;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;

        public ICommand updateCurrentViewModelCommand { get; set; }

        public Navigator(IRootViewModelFactory viewModelAbstractFactory)
        {
            updateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelAbstractFactory);
        }

        public ViewModelBase CurrentViewModel
        {
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
    }
}
