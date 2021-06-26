namespace TutoMVVM.WpfApplication.ViewModels
{
    public class MessageVIewModel : ViewModelBase
    {
        private string _message;

        public bool HasMessage => string.IsNullOrEmpty(Message);

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(nameof(Message)); OnPropertyChanged(nameof(Message)); }
        }
    }
}
