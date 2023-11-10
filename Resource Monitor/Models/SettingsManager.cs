using Newtonsoft.Json;
using ResourceMonitor.ViewModels;
using ResourceMonitor.ViewModels.Resources.CPU;
using System.IO;

namespace ResourceMonitor.Models
{
    public class SettingsManager
    {
        #region Instance
        private static SettingsManager instance = null;
        public static SettingsManager GetInstance()
        {
            if (instance == null)
            {
                instance = new SettingsManager();
            }

            return instance;
        }
        #endregion

        #region Structs
        private struct Settings
        {
            public int RefreshFrequency { get; set; }
            public int HiddenDelay { get; set; }

            public string CPUHardware { get; set; }
            public string CPULoadSensor { get; set; }
            public string CPUTemperatureSensor { get; set; }

            public string GPUHardware { get; set; }
            public string GPULoadSensor { get; set; }
            public string GPUTemperatureSensor { get; set; }

            public string RAMHardware { get; set; }
            public string RAMLoadSensor { get; set;}

            public string DiskHardware { get; set; }
            public string DiskReadSensor { get; set;}
            public string DiskWriteSensor { get; set; }

            public string NetworkHardware { get; set; }
            public string NetworkUploadSensor { get; set; }
            public string NetworkDownloadSensor { get; set; }
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            if (!File.Exists("settings.json"))
            {
                string settings = JsonConvert.SerializeObject(new Settings());
                File.WriteAllText("settings.json", settings);
            }

            ReadSettings();
        }

        public void ReadSettings()
        {
            string settingsString = File.ReadAllText("settings.json");
            Settings settingsClass = JsonConvert.DeserializeObject<Settings>(settingsString);

            var general = GeneralViewModel.GetInstance();
            general.GeneralRefreshFrequency = settingsClass.RefreshFrequency;
            general.GeneralHiddenDelay = settingsClass.HiddenDelay;

            var cpu = CPUViewModel.GetInstance();
            cpu.CPUHardware = settingsClass.CPUHardware;
            cpu.CPULoadSensor = settingsClass.CPULoadSensor;
            cpu.CPUTemperatureSensor = settingsClass.CPUTemperatureSensor;

            var gpu = GPUViewModel.GetInstance();
            gpu.GPUHardware = settingsClass.GPUHardware;
            gpu.GPULoadSensor = settingsClass.GPULoadSensor;
            gpu.GPUTemperatureSensor = settingsClass.GPUTemperatureSensor;

            var ram = RAMViewModel.GetInstance();
            ram.RAMHardware = settingsClass.RAMHardware;
            ram.RAMLoadSensor = settingsClass.RAMLoadSensor;

            var disk = DiskViewModel.GetInstance();
            disk.DiskHardware = settingsClass.DiskHardware;
            disk.DiskReadSensor = settingsClass.DiskReadSensor;
            disk.DiskWriteSensor = settingsClass.DiskWriteSensor;

            var network = NetworkViewModel.GetInstance();
            network.NetworkHardware = settingsClass.NetworkHardware;
            network.NetworkUploadSensor = settingsClass.NetworkUploadSensor;
            network.NetworkDownloadSensor = settingsClass.NetworkDownloadSensor;
        }

        public void WriteSettings()
        {
            Settings settingsClass = new Settings();

            var general = GeneralViewModel.GetInstance();
            settingsClass.RefreshFrequency = general.GeneralRefreshFrequency;
            settingsClass.HiddenDelay = general.GeneralHiddenDelay;

            var cpu = CPUViewModel.GetInstance();
            settingsClass.CPUHardware = cpu.CPUHardware;
            settingsClass.CPULoadSensor = cpu.CPULoadSensor;
            settingsClass.CPUTemperatureSensor = cpu.CPUTemperatureSensor;

            var gpu = GPUViewModel.GetInstance();
            settingsClass.GPUHardware = gpu.GPUHardware;
            settingsClass.GPULoadSensor = gpu.GPULoadSensor;
            settingsClass.GPUTemperatureSensor = gpu.GPUTemperatureSensor;

            var ram = RAMViewModel.GetInstance();
            settingsClass.RAMHardware = ram.RAMHardware;
            settingsClass.RAMLoadSensor = ram.RAMLoadSensor;

            var disk = DiskViewModel.GetInstance();
            settingsClass.DiskHardware = disk.DiskHardware;
            settingsClass.DiskReadSensor = disk.DiskReadSensor;
            settingsClass.DiskWriteSensor = disk.DiskWriteSensor;

            var network = NetworkViewModel.GetInstance();
            settingsClass.NetworkHardware = network.NetworkHardware;
            settingsClass.NetworkUploadSensor = network.NetworkUploadSensor;
            settingsClass.NetworkDownloadSensor = network.NetworkDownloadSensor;

            string settingsString = JsonConvert.SerializeObject(settingsClass);
            File.WriteAllText("settings.json", settingsString);
        }
        #endregion
    }
}
