using ResourceMonitor.Models;
using ResourceMonitor.ViewModels.Resources.CPU;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class CPU : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public CPU()
        {
            InitializeComponent();

            DataContext = CPUViewModel.GetInstance();

            if (ResourceManager.CPU.CPUHardwareList.Count == 0)
            {
                foreach (var hardware in HardwareManager.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Cpu)
                    {
                        ResourceManager.CPU.CPUHardwareList.Add(hardware.Name);
                    }
                }

                if (ResourceManager.CPU.CPUHardware != null)
                {
                    foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.CPU.CPUHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                        {
                            ResourceManager.CPU.CPULoadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                        {
                            ResourceManager.CPU.CPUTemperatureSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void CPUSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ResourceManager.CPU.CPUHardware != null)
            {
                ResourceManager.CPU.CPULoadSensorList.Clear();
                ResourceManager.CPU.CPUTemperatureSensorList.Clear();

                foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.CPU.CPUHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                    {
                        ResourceManager.CPU.CPULoadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                    {
                        ResourceManager.CPU.CPUTemperatureSensorList.Add(sensor.Name);
                    }
                }

                ResourceManager.GPU.GPULoadSensor = null;
                ResourceManager.GPU.GPUTemperatureSensor = null;
            }
        }

        private void CPUHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CPUHardwareListComboBox.SelectionChanged += CPUSelected;
        }
        #endregion
    }
}
