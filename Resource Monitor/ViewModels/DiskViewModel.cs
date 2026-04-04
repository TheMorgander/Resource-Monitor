using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using System;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class DiskViewModel : IResourceViewModel
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
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();

        private string nextDiskReadValue = "--";
        private string nextDiskReadSuffix = "B/s";
        private string nextDiskWriteValue = "--";
        private string nextDiskWriteSuffix = "B/s";
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
        public void Refresh()
        {
            string selectedHardware = DiskHardware;
            string selectedReadSensor = DiskReadSensor;
            string selectedWriteSensor = DiskWriteSensor;

            ISensor diskReadSensor = hardwareScanner.GetSensor(selectedHardware, selectedReadSensor, SensorType.Throughput);
            ISensor diskWriteSensor = hardwareScanner.GetSensor(selectedHardware, selectedWriteSensor, SensorType.Throughput);

            if (diskReadSensor != null && diskReadSensor.Value != null)
            {
                nextDiskReadValue = RoundingConverter.RoundDiskReadValue(ThroughputConverter.ConvertValue((long)diskReadSensor.Value));
                nextDiskReadSuffix = ThroughputConverter.ConvertSuffix((long)diskReadSensor.Value);
            }
            else
            {
                nextDiskReadValue = "--";
                nextDiskReadSuffix = "B/s";
            }

            if (diskWriteSensor != null && diskWriteSensor.Value != null)
            {
                nextDiskWriteValue = RoundingConverter.RoundDiskWriteValue(ThroughputConverter.ConvertValue((long)diskWriteSensor.Value));
                nextDiskWriteSuffix = ThroughputConverter.ConvertSuffix((long)diskWriteSensor.Value);
            }
            else
            {
                nextDiskWriteValue = "--";
                nextDiskWriteSuffix = "B/s";
            }
        }

        public void Apply()
        {
            DiskReadValue = nextDiskReadValue;
            DiskReadSuffix = nextDiskReadSuffix;
            DiskWriteValue = nextDiskWriteValue;
            DiskWriteSuffix = nextDiskWriteSuffix;
        }
        #endregion
    }
}