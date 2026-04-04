using LibreHardwareMonitor.Hardware;
using PropertyChanged;
using ResourceMonitor.Helpers;
using ResourceMonitor.Models;
using System;
using System.Collections.ObjectModel;

namespace ResourceMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class RAMViewModel : IResourceViewModel
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
        private readonly HardwareScanner hardwareScanner = HardwareScanner.GetInstance();

        private string nextRAMLoadValue = "--";
        private string nextRAMLoadSuffix = "%";
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
        public void Refresh()
        {
            string selectedHardware = RAMHardware;
            string selectedLoadSensor = RAMLoadSensor;

            ISensor ramLoadSensor = hardwareScanner.GetSensor(selectedHardware, selectedLoadSensor, SensorType.Load);

            if (ramLoadSensor != null && ramLoadSensor.Value != null)
            {
                nextRAMLoadValue = RoundingConverter.RoundRamLoadValue((double)ramLoadSensor.Value);
                nextRAMLoadSuffix = "%";
            }
            else
            {
                nextRAMLoadValue = "--";
                nextRAMLoadSuffix = "%";
            }
        }

        public void Apply()
        {
            RAMLoadValue = nextRAMLoadValue;
            RAMLoadSuffix = nextRAMLoadSuffix;
        }
        #endregion
    }
}