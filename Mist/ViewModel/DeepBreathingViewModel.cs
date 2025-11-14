using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Mist.ViewModel
{
    public class DeepBreathingViewModel : ViewModelBase
    {
        private const int inhaleTime = 4;
        private const int holdTime = 4;
        private const int exhaleTime = 4;
        private const int repeats = 1;

        private int instructionIndex = 2;
        private int repeat;

        private string instruction;
        public string Instruction
        {
            get { return instruction; }
            set
            {
                instruction = value;
                OnPropertyChanged();
            }
        }

        private int time;
        public int Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        private bool isExerciseComplete;
        public bool IsExerciseComplete
        {
            get { return isExerciseComplete; }
            set
            {
                isExerciseComplete = value;
                OnPropertyChanged();
            }
        }

        private string startButtonText = "Start";
        private string restartButtonText = "Restart";
        private string continueButtonText;
        public string ContinueButtonText
        {
            get { return continueButtonText; }
            set
            {
                continueButtonText = value;
                OnPropertyChanged();
            }
        }

        private List<string> ferretFiles = new List<string> { "inhale", "holdbreath2", "exhale", "holdbreath1" };
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

        private List<string> instructions = new List<string> { "Ready?", "Do exercise again?", "Inhale", "Hold", "Exhale", "Hold"};
        private bool start;

        public RelayCommand ContinueCommand => new RelayCommand(execute => resetExercise());

        public DeepBreathingViewModel()
        {
            Instruction = instructions[0];
            Time = 0;
            IsExerciseComplete = true;
            start = false;
            ContinueButtonText = startButtonText;
            ferretImage = ferretFiles[3];

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Second);
            timer.Interval = new TimeSpan(0, 0, 1); // updates every second
            timer.Start();
        }

        private void UpdateTimer_Second(object sender, EventArgs e)
        {
            if (!start || IsExerciseComplete)
            {
                return;
            }
            Time -= 1;
            if (Time <= 0)
            {
                instructionIndex += 1;
                if (instructionIndex > 5) // Finished exhale
                {
                    if (repeat == 0) // Finished exercise
                    {
                        IsExerciseComplete = true;
                        Instruction = instructions[1];
                        FerretImage = ferretFiles[3];
                        return;
                    } else
                    {
                        repeat -= 1;
                        instructionIndex = 2;
                    }
                }
                FerretImage = ferretFiles[instructionIndex - 2];
                Instruction = instructions[instructionIndex];
                Time = computeNextMaxTime(instructionIndex);
            }
            
        }

        private void resetExercise()
        {
            IsExerciseComplete = false;
            start = true;
            FerretImage = ferretFiles[0];
            Time = inhaleTime;
            instructionIndex = 2;
            Instruction = instructions[instructionIndex];
            repeat = repeats;
            ContinueButtonText = restartButtonText;
        }

        private int computeNextMaxTime(int index)
        {
            switch (index)
            {
                case 3:
                    return holdTime;
                case 4:
                    return exhaleTime;
                default:
                    return inhaleTime;
            }
        }
    }
}
