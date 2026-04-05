using ResourceMonitor.Models;
using ResourceMonitor.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace ResourceMonitor
{
    public partial class SettingsWindow : Window
    {
        #region Fields
        private readonly SettingsManager settingsManager = SettingsManager.GetInstance();
        private readonly MonitoringService monitoringService = MonitoringService.GetInstance();
        private readonly GeneralViewModel generalViewModel = GeneralViewModel.GetInstance();
        #endregion

        #region Constructors
        public SettingsWindow()
        {
            InitializeComponent();

            generalViewModel.PropertyChanged += GeneralViewModelPropertyChanged;
            ApplySettingsVisibility();
        }
        #endregion

        #region Private Methods
        private void ApplySettingsVisibility()
        {
            switch (generalViewModel.DisplayMode)
            {
                case "Network":
                    CPUSettingsControl.Visibility = Visibility.Collapsed;
                    GPUSettingsControl.Visibility = Visibility.Collapsed;
                    RAMSettingsControl.Visibility = Visibility.Collapsed;
                    DiskSettingsControl.Visibility = Visibility.Collapsed;
                    break;

                case "Full":
                default:
                    CPUSettingsControl.Visibility = Visibility.Visible;
                    GPUSettingsControl.Visibility = Visibility.Visible;
                    RAMSettingsControl.Visibility = Visibility.Visible;
                    DiskSettingsControl.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void GeneralViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GeneralViewModel.DisplayMode))
            {
                ApplySettingsVisibility();
            }
        }
        #endregion

        #region Events
        protected override void OnClosed(EventArgs e)
        {
            generalViewModel.PropertyChanged -= GeneralViewModelPropertyChanged;
            base.OnClosed(e);
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                settingsManager.WriteSettings();

                monitoringService.Stop();
                monitoringService.Start();

                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.ApplyLayoutMode();
                }
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