using Mist.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Activity(string name)
        {
            Name = name;
        }
    }
}
