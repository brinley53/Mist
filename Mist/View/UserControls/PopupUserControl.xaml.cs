using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for PopupUserControl.xaml
    /// </summary>
    public partial class PopupUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(PopupUserControl));
        public static readonly DependencyProperty InnerTextProperty = DependencyProperty.Register("InnerText", typeof(string), typeof(PopupUserControl));
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<string>), typeof(PopupUserControl));
        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register("ButtonContent", typeof(string), typeof(PopupUserControl), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(RelayCommand), typeof(PopupUserControl), new PropertyMetadata(null));
        public static readonly DependencyProperty NewActivityNameProperty = DependencyProperty.Register("NewActivityName", typeof(string), typeof(PopupUserControl), 
            new FrameworkPropertyMetadata(
            "",
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty TextboxProperty = DependencyProperty.Register("Textbox", typeof(bool), typeof(PopupUserControl), new PropertyMetadata(false));
        public static readonly DependencyProperty IsViewingProperty = DependencyProperty.Register("IsViewing", typeof(bool), typeof(PopupUserControl),
            new FrameworkPropertyMetadata(
            false,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public PopupUserControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); } 
            set { SetValue(TitleProperty, value); }
        }

        public string InnerText
        {
            get { return (string)GetValue(InnerTextProperty); }
            set { SetValue(InnerTextProperty, value); }
        }

        public List<string> Items
        {
            get { return (List<string>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public bool IsViewing
        {
            get { return (bool)GetValue(IsViewingProperty); }
            set { SetValue(IsViewingProperty, value); }
        }

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public bool Textbox
        {
            get { return (bool)GetValue(TextboxProperty); }
            set { SetValue(TextboxProperty, value); }
        }

        public RelayCommand Command
        {
            get { return (RelayCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public string NewActivityName
        {
            get { return (string)GetValue(NewActivityNameProperty); }
            set { SetValue(NewActivityNameProperty, value); }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            IsViewing = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
