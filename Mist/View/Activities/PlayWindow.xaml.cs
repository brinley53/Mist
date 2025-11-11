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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mist.View.Activities
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        private bool draggingTreat;
        private Point treatPos;

        private PlayViewModel vm;

        public PlayWindow()
        {
            InitializeComponent();
            vm = new PlayViewModel();
            DataContext = vm;
            draggingTreat = false;
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

        private void Treat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var treat = (UIElement)sender;
            var parent = (Canvas)VisualTreeHelper.GetParent(treat);

            // Get mouse position relative to the Canvas, not the Treat
            var mousePos = e.GetPosition(parent);

            // Calculate offset from Treat’s top-left corner
            treatPos = new Point(
                mousePos.X - Canvas.GetLeft(treat),
                mousePos.Y - Canvas.GetTop(treat)
            );

            draggingTreat = true;
            treat.CaptureMouse();
        }

        private void Treat_MouseUp(object sender, MouseButtonEventArgs e)
        {
            draggingTreat = false;
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void Treat_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingTreat)
            {
                var treat = (UIElement)sender;
                var parent = (Canvas)VisualTreeHelper.GetParent(treat);
                var mousePos = e.GetPosition(parent);

                Canvas.SetLeft(treat, mousePos.X - treatPos.X);
                Canvas.SetTop(treat, mousePos.Y - treatPos.Y);
            }
        }
    }
}
