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
using System.Windows.Shell;

namespace Mist.View.UserControls
{
    /// <summary>
    /// Interaction logic for FormUserControl.xaml
    /// </summary>
    public partial class FormUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(FormUserControl));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FormUserControl));
        public static readonly DependencyProperty PromptsProperty = DependencyProperty.Register("Prompts", typeof(List<string>), typeof(FormUserControl));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public List<string> Prompts
        {
            get { return (List<string>)GetValue(PromptsProperty); }
            set { SetValue(PromptsProperty, value); }
        }

        public FormUserControl()
        {
            InitializeComponent();
        }

        private void Prompt_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                Text = Title + " " + (string)button.Content;
            }
        }
    }
}
