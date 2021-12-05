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
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private string username;
        public string UserName
        {
            get { return username; }
            set
            {
                if (username == value)
                {
                    return;
                }
                username = value;
                OnPropertyChanged(new PropertyChangedEventArgs("username"));
            }
        }

        private string userpass;
        public string UserPass
        {
            get { return userpass; }
            set
            {
                if (userpass == value)
                {
                    return;
                }
                userpass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("userpass"));
            }
        }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get { return loginCommand; }
            set
            {
                if (loginCommand == value)
                {
                    return;
                }
                loginCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoginCommand"));
            }
        }

        void LoginExecute(object obj)
        {
            var pass = new HarvestPasswordEventArgs();
            this.HarvestPassword(this, pass);

            if (UserCollection.IsValidUser(UserName.Trim(), pass.Password))
            {
                this.Ok(this, new EventArgs());
            }
            else
            {
                this.Error(this, new EventArgs());
            }
        }

        public LoginWindowViewModel(EventHandler<HarvestPasswordEventArgs> harvestPassword)
        {
            LoginCommand = new RelayCommand(LoginExecute);
            this.HarvestPassword = harvestPassword;
            this.WindowTitle = "Login Window";
        }

        private string windowTitle;
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
        private event EventHandler<HarvestPasswordEventArgs> HarvestPassword;
        public class HarvestPasswordEventArgs : EventArgs
        {
            public string Password;
        }

        public event EventHandler<EventArgs> Ok;


        public event EventHandler<EventArgs> Error;

        
    }
    
}
