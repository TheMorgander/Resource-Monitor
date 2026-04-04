using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceMonitor.Models
{
    public class HardwareScanner
    {
        #region Instance
        private static HardwareScanner instance = null;

        public static HardwareScanner GetInstance()
        {
            if (instance == null)
            {
                instance = new HardwareScanner();
            }

            return instance;
        }
        #endregion

        #region Fields
        private readonly Computer computer = new Computer();
        #endregion

        #region Public Methods
        public void Open()
        {
            computer.IsCpuEnabled = true;
            computer.IsGpuEnabled = true;
            computer.IsMemoryEnabled = true;
            computer.IsStorageEnabled = true;
            computer.IsNetworkEnabled = true;

            computer.Open();
        }

        public void Close()
        {
            computer.Close();
        }

        public void Reset()
        {
            computer.Reset();
        }

        public void Update()
        {
            foreach (IHardware hardware in computer.Hardware)
            {
                hardware.Update();

                foreach (IHardware subHardware in hardware.SubHardware)
                {
                    subHardware.Update();
                }
            }
        }

        public IHardware GetHardware(string hardware, HardwareType hardwareType)
        {
            if (string.IsNullOrWhiteSpace(hardware))
            {
                return null;
            }

            List<IHardware> hardwareList = new List<IHardware>();

            foreach (IHardware topLevelHardware in computer.Hardware)
            {
                hardwareList.Add(topLevelHardware);

                foreach (IHardware subHardware in topLevelHardware.SubHardware)
                {
                    hardwareList.Add(subHardware);
                }
            }

            return hardwareList.FirstOrDefault(h =>
                h.Name == hardware &&
                h.HardwareType == hardwareType);
        }

        public ISensor GetSensor(string hardware, string sensor, SensorType sensorType)
        {
            if (string.IsNullOrWhiteSpace(hardware))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(sensor))
            {
                return null;
            }

            List<IHardware> hardwareList = new List<IHardware>();

            foreach (IHardware topLevelHardware in computer.Hardware)
            {
                hardwareList.Add(topLevelHardware);

                foreach (IHardware subHardware in topLevelHardware.SubHardware)
                {
                    hardwareList.Add(subHardware);
                }
            }

            IHardware selectedHardware = hardwareList.FirstOrDefault(h => h.Name == hardware);

            if (selectedHardware == null)
            {
                return null;
            }

            return selectedHardware.Sensors.FirstOrDefault(s =>
                s.Name == sensor &&
                s.SensorType == sensorType);
        }

        public IHardware[] GetHardwareList()
        {
            List<IHardware> hardwareList = new List<IHardware>();

            foreach (IHardware topLevelHardware in computer.Hardware)
            {
                hardwareList.Add(topLevelHardware);

                foreach (IHardware subHardware in topLevelHardware.SubHardware)
                {
                    hardwareList.Add(subHardware);
                }
            }

            return hardwareList.ToArray();
        }

        public ISensor[] GetSensorList(string hardware)
        {
            if (string.IsNullOrWhiteSpace(hardware))
            {
                return Array.Empty<ISensor>();
            }

            List<IHardware> hardwareList = new List<IHardware>();

            foreach (IHardware topLevelHardware in computer.Hardware)
            {
                hardwareList.Add(topLevelHardware);

                foreach (IHardware subHardware in topLevelHardware.SubHardware)
                {
                    hardwareList.Add(subHardware);
                }
            }

            IHardware selectedHardware = hardwareList.FirstOrDefault(h => h.Name == hardware);

            if (selectedHardware == null)
            {
                return Array.Empty<ISensor>();
            }

            return selectedHardware.Sensors.ToArray();
        }
        #endregion
    }
}