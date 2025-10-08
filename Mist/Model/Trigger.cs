using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mist.Model
{
    public class Trigger : ViewModelBase
    {
        private float _value;
        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        private float threshold;
        public float Threshold
        {
            get { return threshold; }
            set
            {
                threshold = value; 
                OnPropertyChanged();
            }
        }

        public bool RiskCondition()
        {
            return Value >= Threshold;
        }

        public void Increase(float amount)
        {
            Value += amount;
        }

        public void Decrease(float amount)
        {
            Value -= amount;
        }
    }
}
