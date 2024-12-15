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

            SettingsManager.Initialize();
            HardwareManager.Open();
            ResourceManager.Start();

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
            if (SystemParameters.IsRemoteSession == false)
            {
                if (Top == 0) Top = (SystemParameters.PrimaryScreenHeight - this.Height);
                else Top = 0;
            }
            else
            {
                if (Top == 0) Top = (SystemParameters.VirtualScreenHeight - this.Height);
                else Top = 0;
            }
        }
        #endregion
    }
}
