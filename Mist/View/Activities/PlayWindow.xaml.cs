using Mist.ViewModel;
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
using System.Windows.Shapes;

namespace Mist.View.Activities
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {

        private PlayViewModel vm;

        public PlayWindow()
        {
            InitializeComponent();
            vm = new PlayViewModel();
            DataContext = vm;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ferret_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vm.Click = true;
        }

        private void ferret_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            vm.Click = false;
        }

        private void ferret_MouseEnter(object sender, MouseEventArgs e)
        {
            vm.Hover = true;
        }

        private void ferret_MouseLeave(object sender, MouseEventArgs e)
        {
            vm.Hover = false;
        }
    }
}
