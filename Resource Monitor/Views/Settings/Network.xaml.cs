using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class Network : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public Network()
        {
            InitializeComponent();

            DataContext = ResourceManager.Network;

            if (ResourceManager.Network.NetworkHardwareList.Count == 0)
            {
                foreach (var hardware in HardwareManager.GetHardwareList())
                {
                    if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Network)
                    {
                        ResourceManager.Network.NetworkHardwareList.Add(hardware.Name);
                    }
                }

                if (ResourceManager.Network.NetworkHardware != null)
                {
                    foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.Network.NetworkHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            ResourceManager.Network.NetworkUploadSensorList.Add(sensor.Name);
                        }
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                        {
                            ResourceManager.Network.NetworkDownloadSensorList.Add(sensor.Name);
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        private void NetworkSelected(object sender, SelectionChangedEventArgs e)
        {
            if (ResourceManager.Network.NetworkHardware != null)
            {
                ResourceManager.Network.NetworkUploadSensorList.Clear();
                ResourceManager.Network.NetworkDownloadSensorList.Clear();

                foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.Network.NetworkHardware))
                {
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        ResourceManager.Network.NetworkUploadSensorList.Add(sensor.Name);
                    }
                    if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Throughput)
                    {
                        ResourceManager.Network.NetworkDownloadSensorList.Add(sensor.Name);
                    }
                }

                ResourceManager.Network.NetworkUploadSensor = null;
                ResourceManager.Network.NetworkDownloadSensor = null;
            }
        }

        private void NetworkHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NetworkHardwareListComboBox.SelectionChanged += NetworkSelected;
        }
        #endregion
    }
}
