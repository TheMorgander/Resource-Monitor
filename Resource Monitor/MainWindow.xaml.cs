using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (System.Windows.SystemParameters.IsRemoteSession == false) 
            {
                Left = (System.Windows.SystemParameters.PrimaryScreenWidth / 2) - (this.Width / 2);
                Top = 0;
            }
            else
            {
                Left = (System.Windows.SystemParameters.VirtualScreenWidth / 2) - (this.Width / 2);
                Top = 0;
            }
        }
    }
}
