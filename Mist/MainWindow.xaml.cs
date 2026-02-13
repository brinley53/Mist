using Mist.ViewModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mist;

/* 
 Outside sources: Coding Under Pressure
 */

public partial class MainWindow : Window
{
    MainWindowViewModel vm;

    public MainWindow()
    {
        InitializeComponent();
        vm = new MainWindowViewModel();
        DataContext = vm;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        (DataContext as MainWindowViewModel)?.SaveUserData();
        base.OnClosing(e);
    }
}