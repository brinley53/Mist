using Mist.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ActivityUserControl.xaml
    /// </summary>
    public partial class ActivityUserControl : UserControl
    {
        public static readonly DependencyProperty ActivityDataProperty = DependencyProperty.Register("ActivityData", typeof(Activity), typeof(ActivityUserControl));

        public Activity ActivityData
        {
            get { return (Activity)GetValue(ActivityDataProperty); }
            set { SetValue(ActivityDataProperty, value); }
        }

        public ActivityUserControl()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ActivityData.IsEditing = false;
        }
    }
}
