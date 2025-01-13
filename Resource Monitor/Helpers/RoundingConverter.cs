namespace ResourceMonitor.Helpers
{
    public static class RoundingConverter
    {
        #region Public Methods
        public static string RoundCPULoadValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundCPUTempValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundGPULoadValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundGPUTempValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundRamLoadValue(double value)
        {
            return value.ToString("n0");
        }

        public static string RoundDiskReadValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else if (value < 1000)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundDiskWriteValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else if (value < 1000)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundNetworkUploadValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else if (value < 1000)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }

        public static string RoundNetworkDownloadValue(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }
            else if (value < 10)
            {
                return value.ToString("n1");
            }
            else if (value < 100)
            {
                return value.ToString("n1");
            }
            else if (value < 1000)
            {
                return value.ToString("n1");
            }
            else
            {
                return value.ToString("n0");
            }
        }
        #endregion
    }
}
