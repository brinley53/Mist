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

        private bool canEdit;
        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                OnPropertyChanged();
            }
        }

        private bool canDelete;
        public bool CanDelete
        {
            get { return canDelete; }
            set
            {
                canDelete = value;
                OnPropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value; 
                OnPropertyChanged();
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
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

        public Activity(string name, Action? activity = null, bool canEdit = true, bool canDelete = true)
        {
            Name = name;
            ActivityFunction = activity ?? (() => { });
            CanEdit = canEdit;
            IsEditing = false;
            CanDelete = canDelete;
        }
    }
}
