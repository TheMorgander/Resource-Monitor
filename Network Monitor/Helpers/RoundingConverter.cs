namespace NetworkMonitor.Helpers
{
    public static class RoundingConverter
    {
        #region Public Methods
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
