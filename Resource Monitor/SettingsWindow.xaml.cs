using ResourceMonitor.Models;
using System.Windows;

namespace ResourceMonitor
{
    public partial class SettingsWindow : Window
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public SettingsWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            SettingsManager.WriteSettings();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
