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
    internal class ContactFriendViewModel : ViewModelBase
    {
        private List<string> prompts = new List<string> { "overwhelmed", "sad", "angry", "frustrated", "bad" };
        public List<string> Prompts
        {
            get { return prompts; }
        }

        public ContactFriendViewModel()
        {
            
        }
    }
}
