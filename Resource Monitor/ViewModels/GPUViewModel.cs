using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using System;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GPUViewModel : IResourceViewModel
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
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();

        private string nextGPULoadValue = "--";
        private string nextGPULoadSuffix = "%";
        private string nextGPUTemperatureValue = "--";
        private string nextGPUTemperatureSuffix = "°C";
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
        public void Refresh()
        {
            string selectedHardware = GPUHardware;
            string selectedLoadSensor = GPULoadSensor;
            string selectedTemperatureSensor = GPUTemperatureSensor;

            ISensor gpuLoadSensor = hardwareScanner.GetSensor(selectedHardware, selectedLoadSensor, SensorType.Load);
            ISensor gpuTemperatureSensor = hardwareScanner.GetSensor(selectedHardware, selectedTemperatureSensor, SensorType.Temperature);

            if (gpuLoadSensor != null && gpuLoadSensor.Value != null)
            {
                nextGPULoadValue = RoundingConverter.RoundGPULoadValue((double)gpuLoadSensor.Value);
                nextGPULoadSuffix = "%";
            }
            else
            {
                nextGPULoadValue = "--";
                nextGPULoadSuffix = "%";
            }

            if (gpuTemperatureSensor != null && gpuTemperatureSensor.Value != null)
            {
                nextGPUTemperatureValue = RoundingConverter.RoundGPUTempValue((double)gpuTemperatureSensor.Value);
                nextGPUTemperatureSuffix = "°C";
            }
            else
            {
                nextGPUTemperatureValue = "--";
                nextGPUTemperatureSuffix = "°C";
            }
        }

        public void Apply()
        {
            GPULoadValue = nextGPULoadValue;
            GPULoadSuffix = nextGPULoadSuffix;
            GPUTemperatureValue = nextGPUTemperatureValue;
            GPUTemperatureSuffix = nextGPUTemperatureSuffix;
        }
        #endregion
    }
}