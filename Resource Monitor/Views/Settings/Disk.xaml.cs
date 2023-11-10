using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class Disk : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public Disk()
        {
            InitializeComponent();

            DataContext = ResourceManager.Disk;

            if (ResourceManager.Disk.DiskHardwareList.Count == 0)
            {
                foreach (var hardware in HardwareManager.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Storage)
                    {
                        ResourceManager.Disk.DiskHardwareList.Add(hardware.Name);
                    }
                }

                if (ResourceManager.Disk.DiskHardware != null)
                {
                    foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.Disk.DiskHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            ResourceManager.Disk.DiskReadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            ResourceManager.Disk.DiskWriteSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void DiskSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ResourceManager.Disk.DiskHardware != null)
            {
                ResourceManager.Disk.DiskReadSensorList.Clear();
                ResourceManager.Disk.DiskWriteSensorList.Clear();

                foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.Disk.DiskHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        ResourceManager.Disk.DiskReadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        ResourceManager.Disk.DiskWriteSensorList.Add(sensor.Name);
                    }
                }

                ResourceManager.Disk.DiskReadSensor = null;
                ResourceManager.Disk.DiskWriteSensor = null;
            }
        }

        private void DiskHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DiskHardwareListComboBox.SelectionChanged += DiskSelected;
        }
        #endregion
    }
}
