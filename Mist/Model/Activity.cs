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

        private Action<Activity> activityFunction;
        public Action<Activity> ActivityFunction
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

        private bool isViewing;
        public bool IsViewing
        {
            get { return isViewing; }
            set
            {
                isViewing = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ActivityCommand
        {
            get
            {
                return new RelayCommand(execute => ActivityFunction(this));
            }
        }

        public Activity(string name, Action<Activity>? activity = null, bool canEdit = false, bool canDelete = false)
        {
            Name = name;
            ActivityFunction = activity ?? (_ => { });
            CanEdit = canEdit;
            IsEditing = false;
            IsViewing = false;
            CanDelete = canDelete;
        }
    }
}
