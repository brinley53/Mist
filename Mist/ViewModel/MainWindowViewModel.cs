using Mist.Model;
using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Mist.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        Random rnd;

        private Biometrics heartrate;

        // Data variables
        public Biometrics Heartrate
        {
            get { return heartrate; }
            set
            {
                heartrate = value;
                OnPropertyChanged();
            }
        }

        private Biometrics skinResistance;

        public Biometrics SkinResistance {
            get { return skinResistance; }
            set
            {
                skinResistance = value;
                OnPropertyChanged();
            }
        }

        private Biometrics bodyTemperature;
        public Biometrics BodyTemperature
        {
            get { return bodyTemperature; }
            set
            {
                bodyTemperature = value;
                OnPropertyChanged();
            }
        }

        // Stress event variables
        // Via Tomczak et. al
        bool eventOne; // A boolean to determine whether the following stress event is occurring: decrease in resistance is observed at delta t after a pulse increase
        bool eventTwo; // A boolean to determine whether the following stress event is occurring: Temperature decrease is observed at delta t after a resistance decrease
        bool eventThree; // A boolean to determine whether the following stress event is occurring: Temperature reduction is observed at 2 * delta t after a pulse increase

        private int stressLevel;
        public int StressLevel
        {
            get { return stressLevel; }
            set
            {
                stressLevel = value;
                OnPropertyChanged();
            }
        }

        int heartrateEventTimer;
        int resistanceEventTimer;
        int tempEventTimer;

        int deltaT = 10;

        public MainWindowViewModel()
        {
            // Initialize Biometric data variables
            Heartrate = new Biometrics();
            Heartrate.Value = 75f;
            Heartrate.Values = [Heartrate.Value];
            Heartrate.Reference = 75f;
            Heartrate.DifferenceThreshold = Heartrate.Reference * 0.05f;
            Heartrate.DurationCondition = 30f;
            Heartrate.StressIndicationDirection = 1;
            heartrateEventTimer = 0;

            SkinResistance = new Biometrics();
            SkinResistance.Value = 1000f;
            SkinResistance.Values = [SkinResistance.Value];
            SkinResistance.Reference = 1000f;
            SkinResistance.DifferenceThreshold = SkinResistance.Reference * 0.10f;
            SkinResistance.DurationCondition = 60f;
            SkinResistance.StressIndicationDirection = -1;
            resistanceEventTimer = 0;

            BodyTemperature = new Biometrics();
            BodyTemperature.Value = 96.8f;
            BodyTemperature.Values = [BodyTemperature.Value];
            BodyTemperature.Reference = 96.8f;
            BodyTemperature.DifferenceThreshold = 0.1f; //Degrees Celsius
            BodyTemperature.DurationCondition = 60f;
            BodyTemperature.StressIndicationDirection = -1;
            tempEventTimer = 0;

            eventOne = false;
            eventTwo = false;
            eventThree = false;
            stressLevel = 0; // 0 is baseline, no stress; 

            rnd = new Random();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Second);
            timer.Interval = new TimeSpan(0, 0, 1); // updates every second
            timer.Start();

            //// Five second timer
            //DispatcherTimer timer_5sec = new DispatcherTimer();
            //timer_5sec.Tick += new EventHandler(UpdateTimer_FiveSecond);
            //timer_5sec.Interval = new TimeSpan(0, 0, 5); // hours, minutes, seconds
            //timer_5sec.Start();
            // Update the reference values every minute
            // Note: heartrate should have a reference value updated every 30 sec
            //DispatcherTimer timer_minute = new DispatcherTimer();
            //timer_minute.Tick += new EventHandler(update_references);
            //timer_minute.Interval = new TimeSpan(0, 0, 5); // hours, minutes, seconds
            //timer_minute.Start();

            // Delta t timer
            DispatcherTimer delta_t_timer = new DispatcherTimer();
            delta_t_timer.Tick += new EventHandler(UpdateTimer_DeltaT);
            delta_t_timer.Interval = new TimeSpan(0, 0, deltaT); // hours, minutes, seconds
            delta_t_timer.Start();
        }

        private void UpdateTimer_DeltaT(object sender, EventArgs e)
        {
            // Check for change in pulse
            if (Heartrate.StressCondition())
            {
                // Check for stress event one, decrease in resistance after heart increase
                eventOne = heartrateEventTimer == deltaT && SkinResistance.StressCondition();

                // Check for stress event three, decrease in temperature after heart increase
                eventThree = heartrateEventTimer == 2 * deltaT && BodyTemperature.StressCondition();

                heartrateEventTimer += deltaT;
            } else
            {
                // Reset Heartrate variables
                heartrateEventTimer = 0;
                Heartrate.Values = [Heartrate.Value];
                eventOne = false;
                eventThree = false;
            }

            // Check for change in resistance
            if (SkinResistance.StressCondition())
            {
                // Check for stress event two: Temperature decrease is observed at delta t after a resistance decrease
                eventTwo = resistanceEventTimer == deltaT && BodyTemperature.StressCondition();

                resistanceEventTimer += deltaT;
            } else
            {
                // Reset Skin Resistance variables
                resistanceEventTimer = 0;
                SkinResistance.Values = [SkinResistance.Value];
                eventTwo = false;
            }

            // Reset/set temperature variables as needed
            if (BodyTemperature.StressCondition())
            {
                tempEventTimer += deltaT;
            } else
            {
                tempEventTimer = 0;
                BodyTemperature.Values = [BodyTemperature.Value];
            }

            // Reset reference values as needed
            if (Heartrate.LongtermCondition(heartrateEventTimer))
            {
                Heartrate.Reference = Heartrate.Values.Average();
                Heartrate.Values = [Heartrate.Value];
                heartrateEventTimer = 0;
            }

            if (SkinResistance.LongtermCondition(resistanceEventTimer))
            {
                SkinResistance.Reference = SkinResistance.Values.Average();
                SkinResistance.Values = [SkinResistance.Value];
                resistanceEventTimer = 0;
            }

            if (BodyTemperature.LongtermCondition(tempEventTimer))
            {
                BodyTemperature.Reference = BodyTemperature.Values.Average();
                BodyTemperature.Values = [BodyTemperature.Value];
                tempEventTimer = 0;
            }

            // Calculate stress level
            stressLevel = Convert.ToInt32(eventOne) + Convert.ToInt32(eventTwo) + Convert.ToInt32(eventThree);
        }

        private void UpdateTimer_Second(object sender, EventArgs e)
        {
            // Calculate next heartrate value
            int heartrateChange = rnd.Next(1, 6);
            if (Heartrate.Value < 60)
            {
                Heartrate.Value += heartrateChange;
            }
            else if (Heartrate.Value > 200)
            {
                Heartrate.Value -= heartrateChange;
            }
            else
            {
                var signs = new[] { -1, 1 };
                int sign = rnd.Next(2);
                Heartrate.Value += signs[sign] * heartrateChange;
            }
        }

        //private void detect_risk()
        //{
        //    int currentSoundLevel;
        //    int currentPeople;
        //}

        //private void UpdateTimer_FiveSecond(object sender, EventArgs e)
        //{
        //    // Calculate next person value
        //    int currentPeopleCount = Convert.ToInt16(PeopleTextBlock.Text);
        //    int peopleCountChange = rnd.Next(0, 3);
        //    if (currentPeopleCount < 3)
        //    {
        //        currentPeopleCount += peopleCountChange;
        //    }
        //    else
        //    {
        //        var signs = new[] { -1, 1 };
        //        int sign = rnd.Next(2);
        //        currentPeopleCount += signs[sign] * peopleCountChange;
        //    }

        //    // Calculate next sound level value
        //    int currentSoundLevel = rnd.Next(30, 100);


        //    PeopleTextBlock.Text = currentPeopleCount.ToString();
        //    SoundTextBlock.Text = currentSoundLevel.ToString() + " dB";
        //}
    }
}