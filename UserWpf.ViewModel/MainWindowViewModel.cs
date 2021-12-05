using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using UserWpf.Model;

namespace UserWpf.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public void updateList()
        {
            ItemList = ItemCollection.GetAllItem();

            ItemListView = new ListCollectionView(iList);
           ItemListView.Filter = ItemFilter;
        }


        private Item currentItem;
        private ItemCollection iList;
        private ListCollectionView iListView;

        private string filteringText;


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

        public ItemCollection ItemList
        {
            get { return iList; }
            set
            {
                if (iList == value)
                {
                    return;
                }
                iList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemList"));
            }

        }

        public ListCollectionView ItemListView
        {
            get { return iListView; }
            set
            {
                if (iListView == value)
                {
                    return;
                }
                iListView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemListView"));
            }

        }

        public string FilteringText 
        {
            get { return filteringText; }
            set
            {
                if (filteringText == value)
                {
                    return;
                }
                filteringText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilteringText"));
            }

        }

        private ICommand deleteCommand;

        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                if (deleteCommand == value)
                {
                    return;
                }
                deleteCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeleteCommand"));
            }
        }

        void DeleteExecute(object obj)
        {
            CurrentItem.DeleteItem();
            ItemList.Remove(CurrentItem);
        }

        bool CanDelete(object obj)
        {
            if (CurrentItem == null) return false;

            return true;
        }

        public MainWindowViewModel()
        {
            DeleteCommand = new RelayCommand(DeleteExecute, CanDelete);

            this.PropertyChanged += MainWindowViewModel_PropertyChanged;

            ItemList = ItemCollection.GetAllItem();

            ItemListView = new ListCollectionView(ItemList);
            ItemListView.Filter = ItemFilter;

            CurrentItem = new Item();
            LoginUser = new User();
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("FilteringText"))
            {
                ItemListView.Refresh();
            }
        }

        private bool ItemFilter(object obj)
        {
            if (FilteringText == null) return true;
            if (FilteringText.Equals("")) return true;

            Item item = obj as Item;
            return (item.Name.ToLower().StartsWith(FilteringText.ToLower()) || item.Detail.ToLower().StartsWith(FilteringText.ToLower()) || item.Price.ToString().ToLower().StartsWith(FilteringText.ToLower()));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private User loginUser;

        public User LoginUser
        {
            get { return loginUser; }
            set
            {
                if (loginUser == value)
                {
                    return;
                }
                loginUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoginUser"));
            }
        }
    }
}

