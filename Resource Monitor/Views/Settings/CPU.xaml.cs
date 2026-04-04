using ResourceMonitor.Models;
using ResourceMonitor.ViewModels;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class CPU : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        private HardwareScanner hardwareScanner = HardwareScanner.GetInstance();
        #endregion

        #region Constructors
        public CPU()
        {
            InitializeComponent();

            DataContext = CPUViewModel.GetInstance();

            if (monitoringService.CPU.CPUHardwareList.Count == 0)
            {
                foreach (var hardware in hardwareScanner.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Cpu)
                    {
                        monitoringService.CPU.CPUHardwareList.Add(hardware.Name);
                    }
                }

                if (monitoringService.CPU.CPUHardware != null)
                {
                    foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.CPU.CPUHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                        {
                            monitoringService.CPU.CPULoadSensorList.Add(sensor.Name);
                        }

                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                        {
                            monitoringService.CPU.CPUTemperatureSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void CPUSelected(object sender, SelectionChangedEventArgs e)
        {
            if (monitoringService.CPU.CPUHardware != null)
            {
                monitoringService.CPU.CPULoadSensorList.Clear();
                monitoringService.CPU.CPUTemperatureSensorList.Clear();

                foreach (var sensor in hardwareScanner.GetSensorList(monitoringService.CPU.CPUHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                    {
                        monitoringService.CPU.CPULoadSensorList.Add(sensor.Name);
                    }

                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                    {
                        monitoringService.CPU.CPUTemperatureSensorList.Add(sensor.Name);
                    }
                }

                monitoringService.CPU.CPULoadSensor = null;
                monitoringService.CPU.CPUTemperatureSensor = null;
            }
        }

        private void CPUHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CPUHardwareListComboBox.SelectionChanged += CPUSelected;
        }
        #endregion
    }
}