using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserWpf.Model;

namespace UserWpf.ViewModel
{
    public class NewEditWindowViewModel : INotifyPropertyChanged
    {

        private User currentUser;
        private string windowTitle;

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == value)
                {
                    return;
                }
                currentUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentUser"));
            }
        }


        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                if (windowTitle == value)
                {
                    return;
                }
                windowTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("windowTitle"));
            }
        }

        public NewEditWindowViewModel(User user)
        {
            SaveCommand = new RelayCommand(SaveExecute, CanSave);
            CurrentUser = user;
            WindowTitle = "Edit User";
        }

        public NewEditWindowViewModel()
        {
            SaveCommand = new RelayCommand(SaveExecute, CanSave);
            CurrentUser = new User();
            WindowTitle = "New User";
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                if (saveCommand == value)
                {
                    return;
                }
                saveCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SaveCommand"));
            }
        }

        void SaveExecute(object obj)
        {
            if (CurrentUser != null)
            {
                CurrentUser.Save();
                Ok(this, new EventArgs());
            }
        }

        bool CanSave(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public event EventHandler<EventArgs> Ok;
        
    }
}
