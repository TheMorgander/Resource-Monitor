using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources.Full
{
    public partial class RAM : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructors
        public RAM()
        {
            InitializeComponent();

            DataContext = monitoringService.RAM;
        }
        #endregion
    }
}
