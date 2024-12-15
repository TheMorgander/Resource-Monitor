using LibreHardwareMonitor.Hardware;
using System;
using System.Linq;

namespace ResourceMonitor.Models
{
    public class HardwareManager
    {
        #region Instance
        private static HardwareManager instance = null;
        public static HardwareManager GetInstance()
        {
            if (instance == null)
            {
                instance = new HardwareManager();
            }

            return instance;
        }
        #endregion

        #region Fields
        private Computer computer = new Computer();
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
            foreach (var hardware in computer.Hardware)
            {
                hardware.Update();
            }
        }

        public IHardware GetHardware(string hardware, HardwareType hardwareType)
        {
            try
            {
                if (hardware != null)
                {
                    var hard = computer.Hardware.FirstOrDefault(h => (h.Name == hardware) && (h.HardwareType == hardwareType));

                    return hard;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return null;
        }

        public ISensor GetSensor(string hardware, string sensor, SensorType sensorType)
        {
            try
            {
                if (hardware != null && sensor != null)
                {
                    var hard = computer.Hardware.FirstOrDefault(h => h.Name == hardware);
                    var prop = hard.Sensors.FirstOrDefault(p => (p.Name == sensor) && (p.SensorType == sensorType));

                    return prop;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return null;
        }

        public IHardware[] GetHardwareList()
        {
            try
            {
                return computer.Hardware.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return null;
        }

        public ISensor[] GetSensorList(string hardware)
        {
            try
            {
                var hard = computer.Hardware.FirstOrDefault(h => h.Name == hardware);
                return hard.Sensors.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return null;
        }
        #endregion
    }
}
