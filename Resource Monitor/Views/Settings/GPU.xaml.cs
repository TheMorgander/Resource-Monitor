using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class GPU : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public GPU()
        {
            InitializeComponent();

            DataContext = ResourceManager.GPU;

            if (ResourceManager.GPU.GPUHardwareList.Count == 0)
            {
                foreach (var hardware in HardwareManager.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuIntel || 
                        hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuNvidia ||
                        hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.GpuAmd)
                    {
                        ResourceManager.GPU.GPUHardwareList.Add(hardware.Name);
                    }
                }

                if (ResourceManager.GPU.GPUHardware != null)
                {
                    foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.GPU.GPUHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                        {
                            ResourceManager.GPU.GPULoadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                        {
                            ResourceManager.GPU.GPUTemperatureSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void GPUSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ResourceManager.GPU.GPUHardware != null)
            {
                ResourceManager.GPU.GPULoadSensorList.Clear();
                ResourceManager.GPU.GPUTemperatureSensorList.Clear();

                foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.GPU.GPUHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                    {
                        ResourceManager.GPU.GPULoadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Temperature)
                    {
                        ResourceManager.GPU.GPUTemperatureSensorList.Add(sensor.Name);
                    }
                }

                ResourceManager.GPU.GPULoadSensor = null;
                ResourceManager.GPU.GPUTemperatureSensor = null;
            }
        }

        private void GPUHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GPUHardwareListComboBox.SelectionChanged += GPUSelected;
        }
        #endregion
    }
}
