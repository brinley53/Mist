using Mist.Model;
using Mist.MVVM;
using Mist.View.Activities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Media;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Mist.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        Random rnd;

        private Biometric heartrate;

        // Data variables
        public Biometric Heartrate
        {
            get { return heartrate; }
            set
            {
                heartrate = value;
                OnPropertyChanged();
            }
        }

        private Biometric skinResistance;

        public Biometric SkinResistance {
            get { return skinResistance; }
            set
            {
                skinResistance = value;
                OnPropertyChanged();
            }
        }

        private Biometric bodyTemperature;
        public Biometric BodyTemperature
        {
            get { return bodyTemperature; }
            set
            {
                bodyTemperature = value;
                OnPropertyChanged();
            }
        }

        private Model.Trigger soundLevel;
        public Model.Trigger SoundLevel
        {
            get { return soundLevel; }
            set
            {
                soundLevel = value;
                OnPropertyChanged();
            }
        }

        private Model.Trigger lightLevel;
        public Model.Trigger LightLevel
        {
            get { return lightLevel; }
            set
            {
                lightLevel = value;
                OnPropertyChanged();
            }
        }

        // button commands to increase/decrease data
        public RelayCommand HeartIncCommand => new RelayCommand(execute => Heartrate.Increase(5));
        public RelayCommand HeartDecCommand => new RelayCommand(execute => Heartrate.Decrease(5));
        public RelayCommand ResIncCommand => new RelayCommand(execute => SkinResistance.Increase(5000));
        public RelayCommand ResDecCommand => new RelayCommand(execute => SkinResistance.Decrease(5000));
        public RelayCommand TempIncCommand => new RelayCommand(execute => BodyTemperature.Increase(0.1f));
        public RelayCommand TempDecCommand => new RelayCommand(execute => BodyTemperature.Decrease(0.1f));
        public RelayCommand SoundIncCommand => new RelayCommand(execute => SoundLevel.Increase(10f));
        public RelayCommand SoundDecCommand => new RelayCommand(execute => SoundLevel.Decrease(10f));
        public RelayCommand LightIncCommand => new RelayCommand(execute => LightLevel.Increase(500f));
        public RelayCommand LightDecCommand => new RelayCommand(execute => LightLevel.Decrease(500f));
        public RelayCommand Stress1Command => new RelayCommand(execute => GenerateStressEvent(Heartrate));
        public RelayCommand Stress2Command => new RelayCommand(execute => GenerateStressEvent(SkinResistance));
        public RelayCommand Stress3Command => new RelayCommand(execute => GenerateStressEvent(BodyTemperature));
        public RelayCommand SoundRiskCommand => new RelayCommand(execute => GenerateRisk(SoundLevel));
        public RelayCommand LightRiskCommand => new RelayCommand(execute => GenerateRisk(LightLevel));
        public RelayCommand ToggleStressTextVisibility => new RelayCommand(execute => AddressStress());
        public RelayCommand ToggleTriggersVisibility => new RelayCommand(execute => TriggersVisibility = !TriggersVisibility);
        public RelayCommand ToggleActivitiesVisibility => new RelayCommand(execute => ActivitiesVisibility = !ActivitiesVisibility);
        public RelayCommand EditActivityCommand => new RelayCommand(EditActivity);
        public RelayCommand ViewTriggerCommand => new RelayCommand(ViewTrigger);
        
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

        private int stressAddressedTimer = 0;
        private bool stressTextVisibility;
        public bool StressTextVisibility
        {
            get { return stressTextVisibility; }
            set
            {
                stressTextVisibility = value;
                OnPropertyChanged();
            }
        }


        private bool triggersVisibility;
        public bool TriggersVisibility
        {
            get { return triggersVisibility; }
            set
            {
                triggersVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool activitiesVisibility;
        public bool ActivitiesVisibility
        {
            get { return activitiesVisibility; }
            set
            {
                activitiesVisibility = value;
                OnPropertyChanged();
            }
        }

        private int riskLevel;
        public int RiskLevel
        {
            get { return riskLevel; }
            set
            {
                riskLevel = value;
                OnPropertyChanged();
            }
        }

        private List<string> ferretFiles = new List<string> { "ferret", "ferret_stress_1", "ferret_stress_2", "ferret_stress_3" };
        private string ferretImage;
        public string FerretImage
        {
            get { return ferretImage; }
            set
            {
                ferretImage = value;
                OnPropertyChanged();
            }
        }

        private List<String> prompts = new List<string> { "", "Rue is feeling uneasy.", "Rue is feeling tense.", "Rue is very stressed." };
        private List<String> riskPrompts = new List<string> { "loud", "bright" };
        private string ferretText;
        public string FerretText
        {
            get { return ferretText; }
            set
            {
                ferretText = value;
                OnPropertyChanged();
            }
        }

        private string hrt;
        public string HRT
        {
            get { return hrt; }
            set
            {
                hrt = value;
                OnPropertyChanged();
            }
        }

        private string srt;
        public string SRT
        {
            get { return srt; }
            set
            {
                srt = value;
                OnPropertyChanged();
            }
        }

        private string btt;
        public string BTT
        {
            get { return btt; }
            set
            {
                btt = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Model.Trigger> triggers;
        public ObservableCollection<Model.Trigger> Triggers
        {
            get { return triggers; }
            set
            {
                triggers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Activity> activities;
        public ObservableCollection<Activity> Activities
        {
            get { return activities; }
            set
            {
                activities = value;
                OnPropertyChanged();
            }
        }

        private Activity selectedActivity;
        public Activity SelectedActivity
        {
            get { return selectedActivity; }
            set
            {
                selectedActivity = value;
                OnPropertyChanged();
            }
        }

        private Model.Trigger selectedTrigger;
        public Model.Trigger SelectedTrigger
        {
            get { return selectedTrigger; }
            set
            {
                selectedTrigger = value;
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
            Heartrate = new Biometric(75f, 30f, 1);
            Heartrate.DifferenceThreshold = Heartrate.Reference * 0.05f;
            heartrateEventTimer = 0;

            SkinResistance = new Biometric(50000f, 60f, -1); // Value Lower Range: down to 1000 ohms (sweaty). Upper range: 100,000 ohms (dry)
            SkinResistance.DifferenceThreshold = SkinResistance.Reference * 0.10f;
            resistanceEventTimer = 0;

            BodyTemperature = new Biometric(37f, 60f, -1);
            BodyTemperature.DifferenceThreshold = 0.1f; //Degrees Celsius
            tempEventTimer = 0;

            SoundLevel = new Model.Trigger("Sound", 50f, 77.5f); // in decibels; sound levels that start overstimulation: 60-85 dB
            SoundLevel.Mitigations.Add("Find a safer space with less sound.");
            SoundLevel.Mitigations.Add("Turn down the sound.");
            SoundLevel.Mitigations.Add("Use headphones or earplugs.");
            LightLevel = new Model.Trigger("Light", 500f, 1000f); // in Lux. assuming indoor lighting
            LightLevel.Mitigations.Add("Find a safer space with softer light.");
            LightLevel.Mitigations.Add("Turn down the light.");

            SelectedTrigger = SoundLevel;

            HRT = "Heartrate";
            SRT = "Skin Resistance";
            BTT = "Body Temp";

            eventOne = false;
            eventTwo = false;
            eventThree = false;
            stressLevel = 0; // 0 is baseline, no stress; 
            ferretImage = ferretFiles[0];

            Triggers = new ObservableCollection<Model.Trigger>() 
            {
                SoundLevel,
                LightLevel
            };

            Activities = new ObservableCollection<Activity>()
            {
                new Activity("Play with Rue", PlayActivity),
                new Activity("Deep Breathing", DeepBreathingActivity),
                new Activity("Contact a Friend", MessageFriendActivity),
                new Activity("Use Fidget Toys", BaseActivity),
                new Activity("Find a Safe Space", BaseActivity),
                new Activity("Listen to Calming Music", BaseActivity), 
                new Activity("Exercise", BaseActivity)
            };

            SelectedActivity = new Activity("Starter");

            rnd = new Random();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Second);
            timer.Interval = new TimeSpan(0, 0, 1); // updates every second
            timer.Start();

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
                if ((0 < heartrateEventTimer - resistanceEventTimer && heartrateEventTimer - resistanceEventTimer <= deltaT) && SkinResistance.StressCondition())
                {
                    eventOne = true;
                }

                // Check for stress event three, decrease in temperature after heart increase
                if ((0 < heartrateEventTimer - tempEventTimer && heartrateEventTimer - tempEventTimer <= 2 * deltaT) && BodyTemperature.StressCondition())
                {
                    eventThree = true;
                }
            } else
            {
                // Reset Heartrate variables
                if (eventOne || eventThree)
                {
                    Heartrate.Values = [Heartrate.Value];
                }

                heartrateEventTimer = 0;
                HRT = "Heartrate";
            }

            // Check for change in resistance
            if (SkinResistance.StressCondition())
            {
                // Check for stress event two: Temperature decrease is observed at delta t after a resistance decrease
                if ((0 < resistanceEventTimer - tempEventTimer && resistanceEventTimer - tempEventTimer <= deltaT) && BodyTemperature.StressCondition())
                {
                    eventTwo = true;
                }
            } else
            {
                SRT = "Skin Resistance";
                // Reset Skin Resistance variables
                if (eventTwo)
                {
                    SkinResistance.Values = [SkinResistance.Value];
                }
                resistanceEventTimer = 0;
            }

            // Reset/set temperature variables as needed
            if (!BodyTemperature.StressCondition())
            {
                BTT = "Body Temp";
                tempEventTimer = 0;
                if (eventTwo || eventThree)
                {
                    BodyTemperature.Values = [BodyTemperature.Value];
                }
            }

            if (!Heartrate.StressCondition() || !SkinResistance.StressCondition())
            {
                eventOne = false;
            }

            if (!Heartrate.StressCondition() || !BodyTemperature.StressCondition())
            {
                eventThree = false;
            }

            if (!SkinResistance.StressCondition() || !BodyTemperature.StressCondition())
            {
                eventTwo = false;
            }

            // Reset reference values as needed
            if (Heartrate.LongtermCondition(heartrateEventTimer))
            {
                Heartrate.Reference = Heartrate.Values.Average();
                Heartrate.DifferenceThreshold = Heartrate.Reference * 0.05f;
                Heartrate.Values = [Heartrate.Value];
                heartrateEventTimer = 0;
            }

            if (SkinResistance.LongtermCondition(resistanceEventTimer))
            {
                SkinResistance.Reference = SkinResistance.Values.Average();
                SkinResistance.DifferenceThreshold = SkinResistance.Reference * 0.1f;
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
            StressLevel = Convert.ToInt32(eventOne) + Convert.ToInt32(eventTwo) + Convert.ToInt32(eventThree);
            if (stressAddressedTimer > 0)
            {
                stressAddressedTimer -= 1;
                return;
            }
            StressTextVisibility = StressLevel > 0 || RiskLevel > 0;
            if (StressTextVisibility == false)
            {
                TriggersVisibility = false;
                ActivitiesVisibility = false;
            }
            RiskLevel = Convert.ToInt32(SoundLevel.RiskCondition()) + Convert.ToInt32(LightLevel.RiskCondition());

            // update ferret stress indicator
            FerretText = prompts[stressLevel];
            if (RiskLevel > 1)
            {
                FerretText += " Rue noticed it is very loud and bright.";
            } else if (SoundLevel.RiskCondition())
            {
                FerretText += " Rue noticed it is very loud.";
            } else if (LightLevel.RiskCondition())
            {
                FerretText += " Rue noticed it is very bright.";
            }
                FerretImage = ferretFiles[stressLevel];
        }

        private void AddressStress(int duration=120)
        {
            StressTextVisibility = false;
            stressAddressedTimer = duration; // duration in seconds
            TriggersVisibility = false;
            ActivitiesVisibility = false;
        }

        private void BaseActivity()
        {
            // commented out in case individual wants to do more than one thing
            //AddressStress(); 
        }

        private void DeepBreathingActivity()
        {
            DeepBreathingWindow deepBreathingWindow = new DeepBreathingWindow();
            BaseActivity();
            deepBreathingWindow.Show();

            // reference activity logic class
        }

        private void MessageFriendActivity()
        {
            ContactFriendWindow contactFriendWindow = new ContactFriendWindow();
            BaseActivity();
            contactFriendWindow.Show();
        }

        private void PlayActivity()
        {
            PlayWindow playWindow = new PlayWindow();
            BaseActivity();
            playWindow.Show();
        }

        private void GenerateStressEvent(Biometric bio)
        {
            bio.Value = bio.StressCondition() ? bio.Reference : bio.Value + bio.DifferenceThreshold * 4 * bio.StressIndicationDirection;
        }

        private void GenerateRisk(Model.Trigger trig)
        {
            // Toggle a risk condition for the specified Model.Trigger
            trig.Value = trig.RiskCondition() ? trig.Reference : trig.Threshold;
        }

        private void EditActivity(object activity)
        {
            SelectedActivity = (Activity)activity;
            SelectedActivity.IsEditing = true;
        }

        private void AddActivity()
        {
            // EditActivity, but new
        }

        private void ViewTrigger(object trigger)
        {
            SelectedTrigger = (Model.Trigger)trigger;
            SelectedTrigger.IsViewing = true;
        }

        private void UpdateTimer_Second(object sender, EventArgs e)
        {
            // Check for change in pulse
            if (Heartrate.StressCondition())
            {
                heartrateEventTimer += 1;
                HRT = "Heartrate Stress";
            } else
            {
                HRT = "Heartrate";
            }

            // Check for change in resistance
            if (SkinResistance.StressCondition())
            {
                resistanceEventTimer += 1;
                SRT = "Skin Resistance Stress";
            } else
            {
                SRT = "Skin Resistance";
            }

            if (BodyTemperature.StressCondition())
            {
                BTT = "Body Temp Stress";
                tempEventTimer += 1;
            } else
            {
                BTT = "Body Temp";
            }


                // Calculate next heartrate value
            //    int heartrateChange = rnd.Next(0, 4);
            //if (Heartrate.Value < 60)
            //{
            //    Heartrate.Value += heartrateChange;
            //}
            //else if (Heartrate.Value > 200)
            //{
            //    Heartrate.Value -= heartrateChange;
            //}
            //else
            //{
            //    var signs = new[] { -1, 1 };
            //    int sign = rnd.Next(2);
            //    Heartrate.Value += signs[sign] * heartrateChange;
            //}

            Heartrate.Values.Add(Heartrate.Value);
            BodyTemperature.Values.Add(BodyTemperature.Value);
            SkinResistance.Values.Add(SkinResistance.Value);
        }
    }
}