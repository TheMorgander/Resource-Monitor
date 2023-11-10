using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources
{
    public partial class Disk : UserControl
    {
        #region Fields
        private ResourceManager ResourceManager = ResourceManager.GetInstance();
        private HardwareManager HardwareManager = HardwareManager.GetInstance();
        private SettingsManager SettingsManager = SettingsManager.GetInstance();
        #endregion

        #region Constructors
        public Disk()
        {
            InitializeComponent();

            DataContext = ResourceManager.Disk;
        }
        #endregion
    }
}
