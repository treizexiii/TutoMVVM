using TutoMVVM.WpfApplication.State.Navigators;

namespace TutoMVVM.WpfApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }

        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;
            Navigator.updateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}
