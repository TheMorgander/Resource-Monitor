using ResourceMonitor.Models;
using System;
using System.Diagnostics;
using System.Security.Principal;
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
                if (IsRunningAsAdministrator() == false)
                {
                    MessageBox.Show(
                        "Resource Monitor must be run as Administrator.",
                        "Resource Monitor",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    Shutdown();
                    return;
                }

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

        private bool IsRunningAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}