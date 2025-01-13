using ResourceMonitor.Models;
using System;
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion

        #region Events
        private void NetworkSelected(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void NetworkHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NetworkHardwareListComboBox.SelectionChanged += NetworkSelected;
        }
        #endregion
    }
}
