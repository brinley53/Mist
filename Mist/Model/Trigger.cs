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
        private string name = "";
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged();
            }
        }

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

        private bool risk;
        public bool Risk
        {
            get 
            {
                return risk;
            }
            set
            {
                risk = value;
                OnPropertyChanged();
            }
        }

        public bool RiskCondition()
        {
            Risk = Value >= Threshold;
            return Risk;
        }

        public void Increase(float amount)
        {
            Value += amount;
        }

        public void Decrease(float amount)
        {
            Value -= amount;
        }

        public Trigger(string name, float initialValue, float threshold)
        {
            Name = name;
            Value = initialValue;
            Threshold = threshold;
        }
    }
}
