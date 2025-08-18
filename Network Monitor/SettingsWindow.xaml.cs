using NetworkMonitor.Models;
using System;
using System.Windows;

namespace NetworkMonitor
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
            try
            {
                SettingsManager.WriteSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
