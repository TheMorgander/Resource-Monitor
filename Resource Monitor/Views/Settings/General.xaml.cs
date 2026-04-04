using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Settings
{
    public partial class General : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        public General()
        {
            InitializeComponent();

            DataContext = monitoringService.General;
        }
    }
}
