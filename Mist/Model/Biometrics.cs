using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Mist.Model
{
    public class Biometrics : ViewModelBase
    {
        private float _value;
        public float Value {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        private List<float> values = new List<float>();
        public List<float> Values {
            get { return values; }
            set
            {
                values = value;
                OnPropertyChanged();
            }
        }

        private float reference;
        public float Reference {
            get { return reference; }
            set
            {
                reference = value;
                OnPropertyChanged();
            }
        }

        private float differenceThreshold;
        public float DifferenceThreshold {
            get { return differenceThreshold; }
            set
            {
                differenceThreshold = value;
                OnPropertyChanged();
            }
        }

        private float durationCondition;
        public float DurationCondition {
            get { return durationCondition; }
            set
            {
                durationCondition = value;
                OnPropertyChanged();
            }
        }

        // based on whether the biometric reading is elevated or reduced; 1 = elevated, -1 = decreased  
        private int stressIndicationDirection;
        public int StressIndicationDirection
        {
            get { return stressIndicationDirection; }
            set
            {
                stressIndicationDirection = value;
                OnPropertyChanged();
            }
        }

        public bool StressCondition()
        {
            return StressIndicationDirection * (Values.Average() - Reference) > DifferenceThreshold;
        }

        public bool LongtermCondition(float timeElapsed)
        {
            return Math.Abs(Values.Average() - Reference) > DifferenceThreshold && timeElapsed > DurationCondition;
        }
    }
}
