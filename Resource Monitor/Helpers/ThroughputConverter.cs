using System;

namespace ResourceMonitor.Helpers
{
    public static class ThroughputConverter
    {
        #region Constants
        private const double Kilobyte = 1024d;
        private const double Megabyte = Kilobyte * 1024d;
        private const double Gigabyte = Megabyte * 1024d;
        private const double Terabyte = Gigabyte * 1024d;
        #endregion

        #region Public Methods
        public static double ConvertValue(long value)
        {
            if (value < Kilobyte)
            {
                return 0;
            }

            if (value < Megabyte)
            {
                return value / Kilobyte;
            }

            if (value < Gigabyte)
            {
                return value / Megabyte;
            }

            if (value < Terabyte)
            {
                return value / Gigabyte;
            }

            return value / Terabyte;
        }

        public static string ConvertSuffix(long value)
        {
            if (value < Megabyte)
            {
                return "KB/s";
            }

            if (value < Gigabyte)
            {
                return "MB/s";
            }

            if (value < Terabyte)
            {
                return "GB/s";
            }

            return "TB/s";
        }
        #endregion
    }
}