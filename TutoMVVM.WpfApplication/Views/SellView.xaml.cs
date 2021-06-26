using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TutoMVVM.WpfApplication.Views
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : UserControl
    {
        public static readonly DependencyProperty SelectedAssetChangeCommandProperty =
            DependencyProperty.Register("SelectedAssetChangeCommand", typeof(ICommand), typeof(SellView), new PropertyMetadata(null));

        public ICommand SelectedAssetChangeCommand
        {
            get { return (ICommand)GetValue(SelectedAssetChangeCommandProperty); }
            set { SetValue(SelectedAssetChangeCommandProperty, value); }
        }

        public SellView()
        {
            InitializeComponent();
        }

        private void cbAssets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAssets.SelectedItem != null)
            {
                SelectedAssetChangeCommand?.Execute(null);
            }
        }
    }
}
