using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using ResourceMonitor.ViewModels.Resources;
using System;
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
            try
            {
                var gpuLoadSensor = HardwareManager.GetSensor(GPUHardware, GPULoadSensor, SensorType.Load);
                var gpuTemperatureSensor = HardwareManager.GetSensor(GPUHardware, GPUTemperatureSensor, SensorType.Temperature);

                if (gpuLoadSensor != null && gpuLoadSensor.Value != null)
                {
                    GPULoadValue = RoundingConverter.RoundGPULoadValue((double)gpuLoadSensor.Value);
                    GPULoadSuffix = "%";
                }
                else
                {
                    GPULoadValue = "--";
                    GPULoadSuffix = "%";
                }

                if (gpuTemperatureSensor != null && gpuTemperatureSensor.Value != null)
                {
                    GPUTemperatureValue = RoundingConverter.RoundGPUTempValue((double)gpuTemperatureSensor.Value);
                    GPUTemperatureSuffix = "°C";
                }
                else
                {
                    GPUTemperatureValue = "--";
                    GPUTemperatureSuffix = "°C";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion
    }
}
