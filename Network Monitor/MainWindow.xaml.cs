using NetworkMonitor.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace NetworkMonitor
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
            try
            {
                InitializeComponent();

                Process p = Process.GetCurrentProcess();
                p.PriorityClass = ProcessPriorityClass.RealTime;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion

        #region Events
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SettingsWindow SettingsWindow = new SettingsWindow();
                SettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                SettingsWindow.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void OnRightClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    Application.Current.Shutdown();
                    return;
                }

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}
