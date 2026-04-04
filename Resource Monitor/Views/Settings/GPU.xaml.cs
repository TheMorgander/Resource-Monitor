using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class GPU : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        private HardwareScanner hardwareScanner = HardwareScanner.GetInstance();
        #endregion

        #region Constructors
        public GPU()
        {
            InitializeComponent();

            DataContext = monitoringService.GPU;

            if (monitoringService.GPU.GPUHardwareList.Count == 0)
            {
                foreach (var hardware in hardwareScanner.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuIntel ||
                        hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuNvidia ||
                        hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuAmd)
                    {
                        monitoringService.GPU.GPUHardwareList.Add(hardware.Name);
                    }
                }

                if (monitoringService.GPU.GPUHardware != null)
                {
                    foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.GPU.GPUHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                        {
                            monitoringService.GPU.GPULoadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                        {
                            monitoringService.GPU.GPUTemperatureSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void GPUSelected(object sender, SelectionChangedEventArgs e)
        {
            if (monitoringService.GPU.GPUHardware != null)
            {
                monitoringService.GPU.GPULoadSensorList.Clear();
                monitoringService.GPU.GPUTemperatureSensorList.Clear();

                foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.GPU.GPUHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                    {
                        monitoringService.GPU.GPULoadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                    {
                        monitoringService.GPU.GPUTemperatureSensorList.Add(sensor.Name);
                    }
                }

                monitoringService.GPU.GPULoadSensor = null;
                monitoringService.GPU.GPUTemperatureSensor = null;
            }
        }

        private void GPUHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GPUHardwareListComboBox.SelectionChanged += GPUSelected;
        }
        #endregion
    }
}
