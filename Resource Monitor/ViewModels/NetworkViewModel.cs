using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using System;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NetworkViewModel : IResourceViewModel
    {
        #region Instance
        private static NetworkViewModel instance = null;

        public static NetworkViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new NetworkViewModel();
            }

            return instance;
        }
        #endregion

        #region Fields
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();

        private string nextNetworkUploadValue = "--";
        private string nextNetworkUploadSuffix = "B/s";
        private string nextNetworkDownloadValue = "--";
        private string nextNetworkDownloadSuffix = "B/s";
        #endregion

        #region Properties
        public ObservableCollection<string> NetworkHardwareList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> NetworkUploadSensorList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> NetworkDownloadSensorList { get; set; } = new ObservableCollection<string>();

        public string NetworkHardware { get; set; }

        public string NetworkUploadSensor { get; set; }

        public string NetworkDownloadSensor { get; set; }

        public string NetworkUploadValue { get; set; }

        public string NetworkUploadSuffix { get; set; }

        public string NetworkDownloadValue { get; set; }

        public string NetworkDownloadSuffix { get; set; }
        #endregion

        #region Public Methods
        public void Refresh()
        {
            string selectedHardware = NetworkHardware;
            string selectedUploadSensor = NetworkUploadSensor;
            string selectedDownloadSensor = NetworkDownloadSensor;

            ISensor networkUploadSensor = hardwareScanner.GetSensor(selectedHardware, selectedUploadSensor, SensorType.Throughput);
            ISensor networkDownloadSensor = hardwareScanner.GetSensor(selectedHardware, selectedDownloadSensor, SensorType.Throughput);

            if (networkUploadSensor != null && networkUploadSensor.Value != null)
            {
                nextNetworkUploadValue = RoundingConverter.RoundNetworkUploadValue(ThroughputConverter.ConvertValue((long)networkUploadSensor.Value));
                nextNetworkUploadSuffix = ThroughputConverter.ConvertSuffix((long)networkUploadSensor.Value);
            }
            else
            {
                nextNetworkUploadValue = "--";
                nextNetworkUploadSuffix = "B/s";
            }

            if (networkDownloadSensor != null && networkDownloadSensor.Value != null)
            {
                nextNetworkDownloadValue = RoundingConverter.RoundNetworkDownloadValue(ThroughputConverter.ConvertValue((long)networkDownloadSensor.Value));
                nextNetworkDownloadSuffix = ThroughputConverter.ConvertSuffix((long)networkDownloadSensor.Value);
            }
            else
            {
                nextNetworkDownloadValue = "--";
                nextNetworkDownloadSuffix = "B/s";
            }
        }

        public void Apply()
        {
            NetworkUploadValue = nextNetworkUploadValue;
            NetworkUploadSuffix = nextNetworkUploadSuffix;
            NetworkDownloadValue = nextNetworkDownloadValue;
            NetworkDownloadSuffix = nextNetworkDownloadSuffix;
        }
        #endregion
    }
}