using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using NetworkMonitor.Helpers;
using NetworkMonitor.Models;
using NetworkMonitor.ViewModels.Resources;
using System;
using System.Collections.ObjectModel;

namespace NetworkMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NetworkViewModel : IResource
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
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
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

        #region Public Methods`
        public void Update()
        {
            try
            {
                var networkUploadSensor = HardwareManager.GetSensor(NetworkHardware, NetworkUploadSensor, SensorType.Throughput);
                var networkDownloadSensor = HardwareManager.GetSensor(NetworkHardware, NetworkDownloadSensor, SensorType.Throughput);

                if (networkUploadSensor != null && networkUploadSensor.Value != null)
                {
                    NetworkUploadValue = RoundingConverter.RoundNetworkUploadValue(ThroughputConverter.ConvertValue((long)networkUploadSensor.Value));
                    NetworkUploadSuffix = ThroughputConverter.ConvertSuffix((long)networkUploadSensor.Value);
                }
                else
                {
                    NetworkUploadValue = "--";
                    NetworkUploadSuffix = "B/s";
                }

                if (networkDownloadSensor != null && networkDownloadSensor.Value != null)
                {
                    NetworkDownloadValue = RoundingConverter.RoundNetworkDownloadValue(ThroughputConverter.ConvertValue((long)networkDownloadSensor.Value));
                    NetworkDownloadSuffix = ThroughputConverter.ConvertSuffix((long)networkDownloadSensor.Value);
                }
                else
                {
                    NetworkDownloadValue = "--";
                    NetworkDownloadSuffix = "B/s";
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
