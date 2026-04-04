using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class Disk : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        private HardwareScanner hardwareScanner = HardwareScanner.GetInstance();
        #endregion

        #region Constructors
        public Disk()
        {
            InitializeComponent();

            DataContext = monitoringService.Disk;

            if (monitoringService.Disk.DiskHardwareList.Count == 0)
            {
                foreach (var hardware in hardwareScanner.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Storage)
                    {
                        monitoringService.Disk.DiskHardwareList.Add(hardware.Name);
                    }
                }

                if (monitoringService.Disk.DiskHardware != null)
                {
                    foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.Disk.DiskHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            monitoringService.Disk.DiskReadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            monitoringService.Disk.DiskWriteSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void DiskSelected(object sender, SelectionChangedEventArgs e)
        {
            if (monitoringService.Disk.DiskHardware != null)
            {
                monitoringService.Disk.DiskReadSensorList.Clear();
                monitoringService.Disk.DiskWriteSensorList.Clear();

                foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.Disk.DiskHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        monitoringService.Disk.DiskReadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        monitoringService.Disk.DiskWriteSensorList.Add(sensor.Name);
                    }
                }

                monitoringService.Disk.DiskReadSensor = null;
                monitoringService.Disk.DiskWriteSensor = null;
            }
        }

        private void DiskHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DiskHardwareListComboBox.SelectionChanged += DiskSelected;
        }
        #endregion
    }
}
