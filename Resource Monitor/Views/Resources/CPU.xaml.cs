using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources
{
    public partial class CPU : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructors
        public CPU()
        {
            InitializeComponent();

            DataContext = monitoringService.CPU;
        }
        #endregion
    }
}
