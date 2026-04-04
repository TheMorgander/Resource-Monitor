using ResourceMonitor.ViewModels;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class General : UserControl
    {
        public General()
        {
            InitializeComponent();
            DataContext = GeneralViewModel.GetInstance();
        }
    }
}