using ResourceMonitor.Models;
using System;
using System.Diagnostics;
using System.Windows;

namespace ResourceMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SettingsManager settingsManager = SettingsManager.GetInstance();
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();
        private readonly MonitoringService monitoringService = MonitoringService.GetInstance();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                Process process = Process.GetCurrentProcess();
                process.PriorityClass = ProcessPriorityClass.RealTime;

                settingsManager.Initialize();
                hardwareScanner.Open();
                monitoringService.Start();

                MainWindow = new MainWindow();
                MainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The application could not start.\n\n" + ex.Message,
                    "Resource Monitor",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            monitoringService.Stop();
            hardwareScanner.Close();

            base.OnExit(e);
        }
    }
}