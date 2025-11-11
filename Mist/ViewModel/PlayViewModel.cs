using Mist.MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Mist.ViewModel
{
    class PlayViewModel : ViewModelBase
    {
        private List<string> ferretFiles = new List<string> { "ferret_play_0", "ferret_pet_1", "ferret_pet_2", "ferret_pet_3", "ferret_pet_4" };

        private bool hover;
        public bool Hover
        {
            get
            {
                return hover;
            }
            set
            {
                hover = value;
                OnPropertyChanged();
            }
        }

        private bool click;
        public bool Click
        {
            get
            {
                return click;
            }
            set
            {
                click = value;
                OnPropertyChanged();
            }
        }

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

        private int ferretIndex = 0;

        private bool isEatingComplete;
        public bool IsEatingComplete
        {
            get { return isEatingComplete; }
            set
            {
                isEatingComplete = value;
                OnPropertyChanged();
            }
        }

        public PlayViewModel()
        {
            ferretImage = ferretFiles[ferretIndex];

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Second);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500); // updates every second
            timer.Start();
        }

        private void UpdateTimer_Second(object sender, EventArgs e)
        {
            ferretIndex = Hover && Click ? (ferretIndex > 3 ? 1 : ferretIndex + 1) : 0; // If user is petting the ferret, cycle through pics
            FerretImage = ferretFiles[ferretIndex];
        }

    }
}
