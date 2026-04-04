using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources
{
    public partial class Network : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
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
