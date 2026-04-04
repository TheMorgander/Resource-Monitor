using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ResourceMonitor.ViewModels
{
    public class GeneralViewModel : INotifyPropertyChanged
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

        #region Fields
        private int generalRefreshFrequency;
        private bool networkOnlyMode;
        #endregion

        #region Properties
        public int GeneralRefreshFrequency
        {
            get
            {
                return generalRefreshFrequency;
            }
            set
            {
                if (generalRefreshFrequency != value)
                {
                    generalRefreshFrequency = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool NetworkOnlyMode
        {
            get
            {
                return networkOnlyMode;
            }
            set
            {
                if (networkOnlyMode != value)
                {
                    networkOnlyMode = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Methods
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}