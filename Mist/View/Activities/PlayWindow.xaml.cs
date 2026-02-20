using Mist.ViewModel;
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

namespace Mist.View.Activities
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        private bool draggingTreat;
        private Point treatPos;
        private double currentTreatOriginalTop;
        private double currentTreatOriginalLeft;
        private int currentTreats = 6;

        private PlayViewModel vm;

        public PlayWindow()
        {
            InitializeComponent();
            Mouse.OverrideCursor = null;
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
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
        }

        private void ferret_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            vm.Click = false;
            Mouse.OverrideCursor = null;
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

            currentTreatOriginalLeft = Canvas.GetLeft(treat);
            currentTreatOriginalTop = Canvas.GetTop(treat);

            draggingTreat = true;
            treat.CaptureMouse();

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
        }

        private void Treat_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var treat = (UIElement)sender;
            Canvas.SetLeft(treat, currentTreatOriginalLeft);
            Canvas.SetTop(treat, currentTreatOriginalTop);
            draggingTreat = false;
            ((UIElement)sender).ReleaseMouseCapture();

            Mouse.OverrideCursor = null;
        }

        private bool IsColliding(FrameworkElement element1, FrameworkElement element2)
        {
            Rect rect1 = VisualTreeHelper.GetDescendantBounds(element1);
            Rect rect2 = VisualTreeHelper.GetDescendantBounds(element2);

            GeneralTransform transform1 = element1.TransformToVisual(FoodBowl);
            GeneralTransform transform2 = element2.TransformToVisual(Window.GetWindow(element2));

            Rect bounds1 = transform1.TransformBounds(rect1);
            Rect bounds2 = transform2.TransformBounds(rect2);

            return bounds1.IntersectsWith(bounds2);
        }

        private void Treat_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingTreat)
            {
                var treat = (UIElement)sender;
                var parent = (Canvas)VisualTreeHelper.GetParent(treat);
                var mousePos = e.GetPosition(parent);

                if (IsColliding((FrameworkElement)treat, FerretHead))
                { 
                    Canvas.SetLeft(treat, currentTreatOriginalLeft);
                    Canvas.SetTop(treat, currentTreatOriginalTop);
                    treat.Visibility = Visibility.Collapsed; // hide it
                    draggingTreat = false;
                    currentTreats -= 1;
                    vm.FerretImage = "ferret_treat_0";
                    vm.Eating = true;
                    if (currentTreats == 0)
                    {
                        vm.IsEatingComplete = true;
                    }
                } else
                {
                    Canvas.SetLeft(treat, mousePos.X - treatPos.X);
                    Canvas.SetTop(treat, mousePos.Y - treatPos.Y);
                }
            }
        }

        private void RefillButton_Click(object sender, RoutedEventArgs e)
        {
            currentTreats = 6;
            foreach (var treat in FoodBowl.Children.OfType<Image>()) // replace treats
            {
                treat.Visibility = Visibility.Visible;
            }
            vm.IsEatingComplete = false;
        }
    }
}
