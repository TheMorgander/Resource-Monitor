using LibreHardwareMonitor.Hardware;
using ResourceMonitor.Models;
using ResourceMonitor.ViewModels.Resources;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GPUViewModel :IResource
    {
        #region Instance
        private static GPUViewModel instance = null;
        public static GPUViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new GPUViewModel();
            }

            return instance;
        }
        #endregion

        #region Fields
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        #endregion

        #region Properties
        public ObservableCollection<string> GPUHardwareList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> GPULoadSensorList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> GPUTemperatureSensorList { get; set; } = new ObservableCollection<string>();

        public string GPUHardware { get; set; }

        public string GPULoadSensor { get; set; }

        public string GPUTemperatureSensor { get; set; }

        public string GPULoadValue { get; set; }

        public string GPULoadSuffix { get; set; }

        public string GPUTemperatureValue { get; set; }

        public string GPUTemperatureSuffix { get; set; }
        #endregion

        #region Public Methods
        public void Update()
        {
            var gpuLoadSensor = HardwareManager.GetSensor(GPUHardware, GPULoadSensor, SensorType.Load);
            var gpuTemperatureSensor = HardwareManager.GetSensor(GPUHardware, GPUTemperatureSensor, SensorType.Temperature);

            if (gpuLoadSensor != null && gpuLoadSensor.Value != null)
            {
                GPULoadValue = ((int)gpuLoadSensor.Value).ToString();
                GPULoadSuffix = "%";
            }
            else
            {
                GPULoadValue = "--";
                GPULoadSuffix = "%";
            }

            if (gpuTemperatureSensor != null && gpuTemperatureSensor.Value != null)
            {
                GPUTemperatureValue = ((int)gpuTemperatureSensor.Value).ToString();
                GPUTemperatureSuffix = "°C";
            }
            else
            {
                GPUTemperatureValue = "--";
                GPUTemperatureSuffix = "°C";
            }   
        }
        #endregion
    }
}
