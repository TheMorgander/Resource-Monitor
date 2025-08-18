using PropertyChanged;

namespace NetworkMonitor.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class GeneralViewModel
    {
        #region Instance
        private static GeneralViewModel instance = null;
        public static GeneralViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new GeneralViewModel();
            }

            return instance;
        }
        #endregion

        #region Properties
        public int GeneralRefreshFrequency { get; set; }
        #endregion
    }
}
