using ResourceMonitor.Models;
using System;
using System.Windows;

namespace ResourceMonitor
{
    public partial class SettingsWindow : Window
    {
        #region Fields
        private readonly SettingsManager settingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public SettingsWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                settingsManager.WriteSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not save settings.\n\n" + ex.Message,
                    "Resource Monitor",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}