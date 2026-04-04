namespace ResourceMonitor.Helpers
{
    public static class RoundingConverter
    {
        #region Public Methods
        public static string RoundCPULoadValue(double value)
        {
            return RoundPercentage(value);
        }

        public static string RoundCPUTempValue(double value)
        {
            return RoundTemperature(value);
        }

        public static string RoundGPULoadValue(double value)
        {
            return RoundPercentage(value);
        }

        public static string RoundGPUTempValue(double value)
        {
            return RoundTemperature(value);
        }

        public static string RoundRamLoadValue(double value)
        {
            return RoundPercentage(value);
        }

        public static string RoundDiskReadValue(double value)
        {
            return RoundThroughput(value);
        }

        public static string RoundDiskWriteValue(double value)
        {
            return RoundThroughput(value);
        }

        public static string RoundNetworkUploadValue(double value)
        {
            return RoundThroughput(value);
        }

        public static string RoundNetworkDownloadValue(double value)
        {
            return RoundThroughput(value);
        }
        #endregion

        #region Private Methods
        private static string RoundPercentage(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }

            if (value < 100)
            {
                return value.ToString("n1");
            }

            return value.ToString("n0");
        }

        private static string RoundTemperature(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }

            if (value < 100)
            {
                return value.ToString("n1");
            }

            return value.ToString("n0");
        }

        private static string RoundThroughput(double value)
        {
            if (value == 0)
            {
                return value.ToString("n0");
            }

            if (value < 1000)
            {
                return value.ToString("n1");
            }

            return value.ToString("n0");
        }
        #endregion
    }
}