using Mist.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Mist.View.UserControls
{
    /// <summary>
    /// Interaction logic for TriggerUserControl.xaml
    /// </summary>
    public partial class TriggerUserControl : UserControl
    {
        public static readonly DependencyProperty TriggerDataProperty = DependencyProperty.Register("TriggerData", typeof(Model.Trigger), typeof(TriggerUserControl));

        public Model.Trigger TriggerData
        {
            get { return (Model.Trigger)GetValue(TriggerDataProperty); }
            set { SetValue(TriggerDataProperty, value); }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            TriggerData.IsViewing = false;
        }

        public TriggerUserControl()
        {
            InitializeComponent();
        }
    }
}
