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
    public MainWindow()
    {
        InitializeComponent();

        DispatcherTimer timer = new DispatcherTimer();
        timer.Tick += new EventHandler(UpdateTimer_Tick);
        timer.Interval = new TimeSpan(0, 0, 1); // updates every second
        timer.Start();
    }

    private void UpdateTimer_Tick(object sender, EventArgs e)
    {
        Random rnd = new Random();

        // Calculate next heartrate value
        int currentHeartrate = Convert.ToInt16(HeartrateTextBlock.Text);
        int heartrateChange = rnd.Next(1, 6);
        if (currentHeartrate < 60)
        {
            currentHeartrate += heartrateChange;
        } else if (currentHeartrate > 200)
        {
            currentHeartrate -= heartrateChange;
        } else
        {
            var signs = new[] { -1, 1 };
            int sign = rnd.Next(2);
            currentHeartrate += signs[sign]*heartrateChange;
        }

        HeartrateTextBlock.Text = currentHeartrate.ToString();
        SoundTextBlock.Text = DateTime.Now.ToString();
    }
}