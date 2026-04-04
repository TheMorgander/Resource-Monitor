using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using System;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CPUViewModel : IResourceViewModel
    {
        #region Instance
        private static CPUViewModel instance = null;

        public static CPUViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CPUViewModel();
            }

            return instance;
        }
        #endregion

        #region Fields
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();

        private string nextCPULoadValue = "--";
        private string nextCPULoadSuffix = "%";
        private string nextCPUTemperatureValue = "--";
        private string nextCPUTemperatureSuffix = "°C";
        #endregion

        #region Properties
        public ObservableCollection<string> CPUHardwareList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> CPULoadSensorList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> CPUTemperatureSensorList { get; set; } = new ObservableCollection<string>();

        public string CPUHardware { get; set; }

        public string CPULoadSensor { get; set; }

        public string CPUTemperatureSensor { get; set; }

        public string CPULoadValue { get; set; }

        public string CPULoadSuffix { get; set; }

        public string CPUTemperatureValue { get; set; }

        public string CPUTemperatureSuffix { get; set; }
        #endregion

        #region Public Methods
        public void Refresh()
        {
            string selectedHardware = CPUHardware;
            string selectedLoadSensor = CPULoadSensor;
            string selectedTemperatureSensor = CPUTemperatureSensor;

            ISensor cpuLoadSensor = hardwareScanner.GetSensor(selectedHardware, selectedLoadSensor, SensorType.Load);
            ISensor cpuTemperatureSensor = hardwareScanner.GetSensor(selectedHardware, selectedTemperatureSensor, SensorType.Temperature);

            if (cpuLoadSensor != null && cpuLoadSensor.Value != null)
            {
                nextCPULoadValue = RoundingConverter.RoundCPULoadValue((double)cpuLoadSensor.Value);
                nextCPULoadSuffix = "%";
            }
            else
            {
                nextCPULoadValue = "--";
                nextCPULoadSuffix = "%";
            }

            if (cpuTemperatureSensor != null && cpuTemperatureSensor.Value != null)
            {
                nextCPUTemperatureValue = RoundingConverter.RoundCPUTempValue((double)cpuTemperatureSensor.Value);
                nextCPUTemperatureSuffix = "°C";
            }
            else
            {
                nextCPUTemperatureValue = "--";
                nextCPUTemperatureSuffix = "°C";
            }
        }

        public void Apply()
        {
            CPULoadValue = nextCPULoadValue;
            CPULoadSuffix = nextCPULoadSuffix;
            CPUTemperatureValue = nextCPUTemperatureValue;
            CPUTemperatureSuffix = nextCPUTemperatureSuffix;
        }
        #endregion
    }
}