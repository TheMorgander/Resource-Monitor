using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources
{
    public partial class RAM : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public RAM()
        {
            InitializeComponent();

            DataContext = ResourceManager.RAM;
        }
        #endregion
    }
}
