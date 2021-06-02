using System;
using System.Windows.Input;
using TutoMVVM.WpfApplication.State.Navigators;
using TutoMVVM.WpfApplication.ViewModels.Factories;

namespace TutoMVVM.WpfApplication.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IRootViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IRootViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel((ViewType)parameter);
            }
        }
    }
}
