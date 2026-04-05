using ResourceMonitor.Models;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources.Network
{
    public partial class Network : UserControl
    {
        #region Fields
        private readonly MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructors
        public Network()
        {
            InitializeComponent();
            DataContext = monitoringService.Network;
        }
        #endregion
    }
}