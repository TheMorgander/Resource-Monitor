using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Models;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels.Resources.CPU
{
    [AddINotifyPropertyChangedInterface]
    public class CPUViewModel : IResource
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
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
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

        public string CPUTemperatureSuffix { get; set;}
        #endregion

        #region Public Methods
        public void Update()
        {
            var cpuLoadSensor = HardwareManager.GetSensor(CPUHardware, CPULoadSensor, SensorType.Load);
            var cpuTemperatureSensor = HardwareManager.GetSensor(CPUHardware, CPUTemperatureSensor, SensorType.Temperature);

            if (cpuLoadSensor != null && cpuLoadSensor.Value != null)
            { 
                CPULoadValue = ((int)cpuLoadSensor.Value).ToString();
                CPULoadSuffix = "%";
            }
            else
            {
                CPULoadValue = "--";
                CPULoadSuffix = "%";
            }

            if (cpuTemperatureSensor != null && cpuTemperatureSensor.Value != null)
            {
                CPUTemperatureValue = ((int)cpuTemperatureSensor.Value).ToString();
                CPUTemperatureSuffix = "°C";
            }
            else
            {
                CPUTemperatureValue = "--";
                CPUTemperatureSuffix = "°C";
            } 
        }
        #endregion
    }
}
