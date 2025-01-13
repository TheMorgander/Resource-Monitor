using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class RAM : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public RAM()
        {
            try
            {
                InitializeComponent();

                DataContext = ResourceManager.RAM;

                if (ResourceManager.RAM.RAMHardwareList.Count == 0)
                {
                    foreach (var hardware in HardwareManager.GetHardwareList())
                    {
                        if (hardware.HardwareType == LibreHardwareMonitor.Hardware.HardwareType.Memory)
                        {
                            ResourceManager.RAM.RAMHardwareList.Add(hardware.Name);
                        }
                    }

                    if (ResourceManager.RAM.RAMHardware != null)
                    {
                        foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.RAM.RAMHardware))
                        {
                            if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                            {
                                ResourceManager.RAM.RAMLoadSensorList.Add(sensor.Name);
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
        private void RAMSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ResourceManager.RAM.RAMHardware != null)
                {
                    ResourceManager.RAM.RAMLoadSensorList.Clear();

                    foreach (var sensor in HardwareManager.GetSensorList(ResourceManager.RAM.RAMHardware))
                    {
                        if (sensor.SensorType == LibreHardwareMonitor.Hardware.SensorType.Load)
                        {
                            ResourceManager.RAM.RAMLoadSensorList.Add(sensor.Name);
                        }
                    }

                    ResourceManager.RAM.RAMLoadSensor = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void RAMHardwareListComboBoxLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            RAMHardwareListComboBox.SelectionChanged += RAMSelected;
        }
        #endregion
    }
}
