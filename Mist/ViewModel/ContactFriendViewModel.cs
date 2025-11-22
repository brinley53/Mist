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
        private List<string> peoplePrompts = new List<string> { "Mom", "Dad", "brother", "sister", "friend", "partner" };
        public List<string> PeoplePrompts
        {
            get { return peoplePrompts; }
        }

        private List<string> feelingPrompts = new List<string> { "overstimulated", "overwhelmed", "sad", "angry", "frustrated", "bad", "confused", "annoyed", "upset", "distressed" };
        public List<string> FeelingPrompts
        {
            get { return feelingPrompts; }
        }

        private List<string> reasonPrompts = new List<string> { "sensory issues", "the loud noises", "the bright lights", "the textures I feel", "the people around me", "something that I can't identify" };
        public List<string> ReasonPrompts
        {
            get { return reasonPrompts; }
        }

        private List<string> actionPrompts = new List<string> { "a break", "to be left alone", "help addressing the issue", "to go somewhere else", "less noise", "to go home", "to do something else" };
        public List<string> ActionPrompts
        {
            get { return actionPrompts; }
        }

        private string people;
        public string People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged();
                SetMessage();
            }
        }

        private string feeling;
        public string Feeling
        {
            get { return feeling; }
            set { 
                feeling = value;
                OnPropertyChanged();
                SetMessage();
            }
        }

        private string reason;
        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                OnPropertyChanged();
                SetMessage();
            }
        }

        private string action;
        public string Action
        {
            get { return action; }
            set
            {
                action = value;
                OnPropertyChanged();
                SetMessage();
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public ContactFriendViewModel()
        {
            People = "";
            Feeling = "";
            Reason = "";
            Action = "";
            Message = "";
        }

        private void SetMessage()
        {
            Message = People;
            if (Message != "")
            {
                Message += ", \n\n";
            }
            Message += Feeling + " " + Reason;
            if (Reason != "" || Feeling != "")
            {
                Message += ". ";
            }
            Message += Action;
            if (Action != "")
            {
                Message += ".";
            }
        }
    }
}
