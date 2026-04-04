using ResourceMonitor.Models;
using System;
using System.Windows.Controls;

namespace ResourceMonitor.Views.Resources
{
    public partial class Disk : UserControl
    {
        #region Fields
        private MonitoringService monitoringService = MonitoringService.GetInstance();
        #endregion

        #region Constructors
        public Disk()
        {
            InitializeComponent();

            DataContext = monitoringService.Disk;
        }
        #endregion
    }
}
