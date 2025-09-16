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
    Random rnd;

    public MainWindow()
    {
        InitializeComponent();
        rnd = new Random();

        DispatcherTimer timer = new DispatcherTimer();
        timer.Tick += new EventHandler(UpdateTimer_Second);
        timer.Interval = new TimeSpan(0, 0, 1); // updates every second
        timer.Start();

        // Five second timer
        DispatcherTimer timer_5sec = new DispatcherTimer();
        timer_5sec.Tick += new EventHandler(UpdateTimer_FiveSecond);
        timer_5sec.Interval = new TimeSpan(0, 0, 5); // hours, minutes, seconds
        timer_5sec.Start();
    }

    private void Timer_5sec_Tick(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void UpdateTimer_Second(object sender, EventArgs e)
    {
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
    }

    private void UpdateTimer_FiveSecond(object sender, EventArgs e)
    {
        // Calculate next person value
        int currentPeopleCount = Convert.ToInt16(PeopleTextBlock.Text);
        int peopleCountChange = rnd.Next(0, 3);
        if (currentPeopleCount < 3)
        {
            currentPeopleCount += peopleCountChange;
        }
        else
        {
            var signs = new[] { -1, 1 };
            int sign = rnd.Next(2);
            currentPeopleCount += signs[sign] * peopleCountChange;
        }

        // Calculate next sound level value
        int currentSoundLevel = rnd.Next(30, 100);


        PeopleTextBlock.Text = currentPeopleCount.ToString();
        SoundTextBlock.Text = currentSoundLevel.ToString() + " dB";
    }
}