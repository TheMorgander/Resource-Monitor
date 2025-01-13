using System;

namespace ResourceMonitor.Helpers
{
    public static class ThroughputConverter
    {
        #region Public Methods
        public static double ConvertValue(long value)
        {
            if (value < Math.Pow(1024, 1))
            {
                return 0;
            }
            else if (value < Math.Pow(1024, 2))
            {
                return value / Math.Pow(1024, 1);
            }
            else if (value < Math.Pow(1024, 3))
            {
                return value / Math.Pow(1024, 2);
            }
            else if (value < Math.Pow(1024, 4))
            {
                return value / Math.Pow(1024, 3);
            }
            else
            {
                return value / Math.Pow(1024, 4);
            }
        }

        public static string ConvertSuffix(long value)
        {
            if (value < Math.Pow(1024, 2))
            {
                return "KB/s";
            }
            else if (value < Math.Pow(1024, 3))
            {
                return "MB/s";
            }
            else if (value < Math.Pow(1024, 4))
            {
                return "GB/s";
            }
            else
            {
                return "TB/s";
            }
        }
        #endregion
    }
}
