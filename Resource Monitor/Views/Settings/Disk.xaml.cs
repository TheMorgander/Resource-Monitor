using ResourceMonitor.Models;
using System;
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion

        #region Events
        private void DiskSelected(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void DiskHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DiskHardwareListComboBox.SelectionChanged += DiskSelected;
        }
        #endregion
    }
}
