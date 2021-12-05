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

        private Item currentItem;
        private string windowTitle;

        public Item CurrentItem
        {
            get { return currentItem; }
            set
            {
                if (currentItem == value)
                {
                    return;
                }
                currentItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentItem"));
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

        public NewEditWindowViewModel(Item item)
        {
            SaveCommand = new RelayCommand(SaveExecute, CanSave);
            CurrentItem = item;
            WindowTitle = "Edit Item";
        }

        public NewEditWindowViewModel()
        {
            SaveCommand = new RelayCommand(SaveExecute, CanSave);
            CurrentItem = new Item();
            WindowTitle = "New Item";
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
            if (CurrentItem != null)
            {
                CurrentItem.Save();
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
