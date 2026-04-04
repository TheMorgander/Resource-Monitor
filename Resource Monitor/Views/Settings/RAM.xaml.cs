using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class RAM : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        private HardwareScanner hardwareScanner = HardwareScanner.GetInstance();
        #endregion

        #region Constructors
        public RAM()
        {
            InitializeComponent();

            DataContext = monitoringService.RAM;

            if (monitoringService.RAM.RAMHardwareList.Count == 0)
            {
                foreach (var hardware in hardwareScanner.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Memory)
                    {
                        monitoringService.RAM.RAMHardwareList.Add(hardware.Name);
                    }
                }

                if (monitoringService.RAM.RAMHardware != null)
                {
                    foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.RAM.RAMHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                        {
                            monitoringService.RAM.RAMLoadSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void RAMSelected(object sender, SelectionChangedEventArgs e)
        {
            if (monitoringService.RAM.RAMHardware != null)
            {
                monitoringService.RAM.RAMLoadSensorList.Clear();

                foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.RAM.RAMHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                    {
                        monitoringService.RAM.RAMLoadSensorList.Add(sensor.Name);
                    }
                }

                monitoringService.RAM.RAMLoadSensor = null;
            }
        }

        private void RAMHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            RAMHardwareListComboBox.SelectionChanged += RAMSelected;
        }
        #endregion
    }
}
