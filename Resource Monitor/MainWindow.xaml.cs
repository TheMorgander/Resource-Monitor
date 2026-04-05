using ResourceMonitor.Models;
using ResourceMonitor.ViewModels;
using ResourceMonitor.Views.Modes;
using System;
using System.Windows;
using System.Windows.Input;

namespace ResourceMonitor
{
    public partial class MainWindow : Window
    {
        #region Fields
        private readonly GeneralViewModel generalViewModel = GeneralViewModel.GetInstance();
        private readonly SettingsManager settingsManager = SettingsManager.GetInstance();
        private readonly MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            ApplyLayoutMode();
        }
        #endregion

        #region Public Methods
        public void ApplyLayoutMode()
        {
            switch (generalViewModel.DisplayMode)
            {
                case "Network":
                    ModeContentControl.Content = new NetworkMode();
                    Width = 130;
                    Height = 33;
                    break;

                case "Full":
                default:
                    ModeContentControl.Content = new FullMode();
                    Width = 325;
                    Height = 45;
                    break;
            }

            double screenWidth;

            if (SystemParameters.IsRemoteSession)
            {
                screenWidth = SystemParameters.VirtualScreenWidth;
            }
            else
            {
                screenWidth = SystemParameters.PrimaryScreenWidth;
            }

            Left = (screenWidth / 2) - (Width / 2);

            switch (generalViewModel.WindowPosition)
            {
                case "Bottom":
                    Top = SystemParameters.WorkArea.Height - Height;
                    break;

                case "Top":
                default:
                    Top = 0;
                    break;
            }
        }
        #endregion

        #region Private Methods
        private string GetNextDisplayMode()
        {
            switch (generalViewModel.DisplayMode)
            {
                case "Full":
                    return "Network";

                case "Network":
                default:
                    return "Full";
            }
        }
        #endregion

        #region Events
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    generalViewModel.DisplayMode = GetNextDisplayMode();

                    settingsManager.WriteSettings();

                    monitoringService.Stop();
                    monitoringService.Start();

                    ApplyLayoutMode();
                    return;
                }

                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                settingsWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not change mode.\n\n" + ex.Message,
                    "Resource Monitor",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void OnRightClick(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                Application.Current.Shutdown();
                return;
            }

            switch (generalViewModel.WindowPosition)
            {
                case "Top":
                    generalViewModel.WindowPosition = "Bottom";
                    break;

                case "Bottom":
                default:
                    generalViewModel.WindowPosition = "Top";
                    break;
            }

            settingsManager.WriteSettings();
            ApplyLayoutMode();
        }
        #endregion
    }
}