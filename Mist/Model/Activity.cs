using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Mist.Model
{
    public class Activity : ViewModelBase
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

        private Action activityFunction;
        public Action ActivityFunction
        {
            get { return activityFunction; }
            set
            {
                activityFunction = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ActivityCommand
        {
            get
            {
                return new RelayCommand(execute => ActivityFunction());
            }
        }

        public Activity(string name, Action? activity = null)
        {
            Name = name;
            ActivityFunction = activity ?? (() => { });
        }
    }
}
