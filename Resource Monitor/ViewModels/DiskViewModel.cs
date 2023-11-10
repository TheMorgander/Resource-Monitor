using LibreHardwareMonitor.Hardware;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using ResourceMonitor.ViewModels.Resources;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class DiskViewModel : IResource
    {
        #region Instance
        private static DiskViewModel instance = null;
        public static DiskViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new DiskViewModel();
            }

            return instance;
        }
        #endregion

        #region Fields
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        #endregion

        #region Properties
        public ObservableCollection<string> DiskHardwareList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> DiskReadSensorList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> DiskWriteSensorList { get; set; } = new ObservableCollection<string>();

        public string DiskHardware { get; set; }

        public string DiskReadSensor { get; set; }

        public string DiskWriteSensor { get; set; }

        public string DiskReadValue { get; set; }

        public string DiskReadSuffix { get; set; }

        public string DiskWriteValue { get; set; }

        public string DiskWriteSuffix { get; set; }
        #endregion

        #region Public Methods
        public void Update()
        {
            var diskReadSensor = HardwareManager.GetSensor(DiskHardware, DiskReadSensor, SensorType.Throughput);
            var diskWriteSensor = HardwareManager.GetSensor(DiskHardware, DiskWriteSensor, SensorType.Throughput);

            if (diskReadSensor != null && diskReadSensor.Value != null) 
            { 
                DiskReadValue = ThroughputConverter.ConvertValue((long)diskReadSensor.Value).ToString();
                DiskReadSuffix = ThroughputConverter.ConvertSuffix((long)diskReadSensor.Value);
            }
            else
            {
                DiskReadValue = "--";
                DiskReadSuffix = "B/s";
            }

            if (diskWriteSensor != null && diskWriteSensor.Value != null)
            {
                DiskWriteValue = ThroughputConverter.ConvertValue((long)diskWriteSensor.Value).ToString();
                DiskWriteSuffix = ThroughputConverter.ConvertSuffix((long)diskWriteSensor.Value);
            }
            else
            {
                DiskWriteValue = "--";
                DiskWriteSuffix = "B/s";
            }
        }
        #endregion
    }
}
