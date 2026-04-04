using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class Network : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        private HardwareScanner hardwareScanner = HardwareScanner.GetInstance();
        #endregion

        #region Constructors
        public Network()
        {
            InitializeComponent();

            DataContext = monitoringService.Network;

            if (monitoringService.Network.NetworkHardwareList.Count == 0)
            {
                foreach (var hardware in hardwareScanner.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Network)
                    {
                        monitoringService.Network.NetworkHardwareList.Add(hardware.Name);
                    }
                }

                if (monitoringService.Network.NetworkHardware != null)
                {
                    foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.Network.NetworkHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            monitoringService.Network.NetworkUploadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            monitoringService.Network.NetworkDownloadSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void NetworkSelected(object sender, SelectionChangedEventArgs e)
        {
            if (monitoringService.Network.NetworkHardware != null)
            {
                monitoringService.Network.NetworkUploadSensorList.Clear();
                monitoringService.Network.NetworkDownloadSensorList.Clear();

                foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.Network.NetworkHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        monitoringService.Network.NetworkUploadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        monitoringService.Network.NetworkDownloadSensorList.Add(sensor.Name);
                    }
                }

                monitoringService.Network.NetworkUploadSensor = null;
                monitoringService.Network.NetworkDownloadSensor = null;
            }
        }

        private void NetworkHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NetworkHardwareListComboBox.SelectionChanged += NetworkSelected;
        }
        #endregion
    }
}
