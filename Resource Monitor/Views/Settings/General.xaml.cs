using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class General : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        public General()
        {
            InitializeComponent();

            DataContext = ResourceManager.General;
        }
    }
}
