using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources
{
    public partial class NetworkOnly : UserControl
    {
        #region Fields
        private readonly MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructors
        public NetworkOnly()
        {
            InitializeComponent();
            DataContext = monitoringService.Network;
        }
        #endregion
    }
}