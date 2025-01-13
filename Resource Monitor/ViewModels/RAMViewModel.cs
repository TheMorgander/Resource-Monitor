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
    public class RAMViewModel : IResource
    {
        #region Instance
        private static RAMViewModel instance = null;
        public static RAMViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new RAMViewModel();
            }

            return instance;
        }
        #endregion

        #region Fields
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        #endregion

        #region Properties
        public ObservableCollection<string> RAMHardwareList { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> RAMLoadSensorList { get; set; } = new ObservableCollection<string>();

        public string RAMHardware { get; set; }

        public string RAMLoadSensor { get; set; }

        public string RAMLoadValue { get; set; }

        public string RAMLoadSuffix { get; set; } 
        #endregion

        #region Public Methods
        public void Update()
        {
            try
            {
                var ramLoadSensor = HardwareManager.GetSensor(RAMHardware, RAMLoadSensor, SensorType.Load);

                if (ramLoadSensor != null && ramLoadSensor.Value != null)
                {
                    RAMLoadValue = RoundingConverter.RoundRamLoadValue((double)ramLoadSensor.Value);
                    RAMLoadSuffix = "%";
                }
                else
                {
                    RAMLoadValue = "--";
                    RAMLoadSuffix = "%";
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
