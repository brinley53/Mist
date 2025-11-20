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
    /// Interaction logic for ContactFriendWindow.xaml
    /// </summary>
    public partial class ContactFriendWindow : Window
    {
        public ContactFriendWindow()
        {
            InitializeComponent();
            ContactFriendViewModel vm = new ContactFriendViewModel();
            DataContext = vm;
        }
    }
}
