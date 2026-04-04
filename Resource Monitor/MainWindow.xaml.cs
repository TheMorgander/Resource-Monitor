using System;
using System.Windows;
using System.Windows.Input;

namespace ResourceMonitor
{
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            SetInitialWindowPosition();
        }
        #endregion

        #region Private Methods
        private void SetInitialWindowPosition()
        {
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
            Top = 0;
        }
        #endregion

        #region Events
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                settingsWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not open settings.\n\n" + ex.Message,
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

            if (Top == 0)
            {
                Top = SystemParameters.WorkArea.Height - Height;
            }
            else
            {
                Top = 0;
            }
        }
        #endregion
    }
}