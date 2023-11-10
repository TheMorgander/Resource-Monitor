using ResourceMonitor.Models;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ResourceMonitor
{
    public partial class MainWindow : Window
    {
        #region Fields
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            if (SystemParameters.IsRemoteSession == false) 
            {
                Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
                Top = 0;
            }
            else
            {
                Left = (SystemParameters.VirtualScreenWidth / 2) - (this.Width / 2);
                Top = 0;
            }

            SettingsManager.Initialize();
            HardwareManager.Open();
            ResourceManager.Start();
        }
        #endregion

        #region Events
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SettingsWindow SettingsWindow = new SettingsWindow();
            SettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SettingsWindow.Show();
        }

        private void OnRightClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            Thread.Sleep(ResourceManager.General.GeneralHiddenDelay);
            Application.Current.MainWindow.Show();
        }
        #endregion
    }
}
