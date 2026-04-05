using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources.Full
{
    public partial class GPU : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructors
        public GPU()
        {
            InitializeComponent();

            DataContext = monitoringService.GPU;
        }
        #endregion
    }
}
