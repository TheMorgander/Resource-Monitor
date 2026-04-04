using ResourceMonitor.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace ResourceMonitor
{
    public partial class MainWindow : Window
    {
        #region Fields
        private readonly GeneralViewModel generalViewModel = GeneralViewModel.GetInstance();
        private bool hasBeenPositioned = false;
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
            bool isAtTop;

            if (hasBeenPositioned == false)
            {
                isAtTop = generalViewModel.NetworkOnlyMode == false;
            }
            else
            {
                isAtTop = Top == 0;
            }

            if (generalViewModel.NetworkOnlyMode)
            {
                FullMonitorLayout.Visibility = Visibility.Collapsed;
                NetworkOnlyLayout.Visibility = Visibility.Visible;

                Width = 130;
                Height = 33;
            }
            else
            {
                FullMonitorLayout.Visibility = Visibility.Visible;
                NetworkOnlyLayout.Visibility = Visibility.Collapsed;

                Width = 325;
                Height = 45;
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

            if (isAtTop)
            {
                Top = 0;
            }
            else
            {
                Top = SystemParameters.WorkArea.Height - Height;
            }

            hasBeenPositioned = true;
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