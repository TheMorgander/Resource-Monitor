using Newtonsoft.Json;
using NetworkMonitor.ViewModels;
using System;
using System.IO;

namespace NetworkMonitor.Models
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

            public string NetworkHardware { get; set; }
            public string NetworkUploadSensor { get; set; }
            public string NetworkDownloadSensor { get; set; }
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            try
            {
                if (!File.Exists("network monitor settings.json"))
                {
                    string settings = JsonConvert.SerializeObject(new Settings());
                    File.WriteAllText("network monitor settings.json", settings);
                }

                ReadSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void ReadSettings()
        {
            try
            {
                string settingsString = File.ReadAllText("network monitor settings.json");
                Settings settingsClass = JsonConvert.DeserializeObject<Settings>(settingsString);

                var general = GeneralViewModel.GetInstance();
                general.GeneralRefreshFrequency = settingsClass.RefreshFrequency;

                var network = NetworkViewModel.GetInstance();
                network.NetworkHardware = settingsClass.NetworkHardware;
                network.NetworkUploadSensor = settingsClass.NetworkUploadSensor;
                network.NetworkDownloadSensor = settingsClass.NetworkDownloadSensor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void WriteSettings()
        {
            try
            {
                Settings settingsClass = new Settings();

                var general = GeneralViewModel.GetInstance();
                settingsClass.RefreshFrequency = general.GeneralRefreshFrequency;

                var network = NetworkViewModel.GetInstance();
                settingsClass.NetworkHardware = network.NetworkHardware;
                settingsClass.NetworkUploadSensor = network.NetworkUploadSensor;
                settingsClass.NetworkDownloadSensor = network.NetworkDownloadSensor;

                string settingsString = JsonConvert.SerializeObject(settingsClass);
                File.WriteAllText("network monitor settings.json", settingsString);
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
