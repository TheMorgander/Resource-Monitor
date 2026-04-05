using System.Collections.ObjectModel;
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
        private string displayMode = "Full";
        private string windowPosition = "Top";
        #endregion

        #region Constructor
        private GeneralViewModel()
        {
            DisplayModeOptions = new ObservableCollection<string>
            {
                "Full",
                "Network"
            };

            WindowPositionOptions = new ObservableCollection<string>
            {
                "Top",
                "Bottom"
            };
        }
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

        public string DisplayMode
        {
            get
            {
                return displayMode;
            }
            set
            {
                if (displayMode != value)
                {
                    displayMode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WindowPosition
        {
            get
            {
                return windowPosition;
            }
            set
            {
                if (windowPosition != value)
                {
                    windowPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> DisplayModeOptions { get; }

        public ObservableCollection<string> WindowPositionOptions { get; }
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