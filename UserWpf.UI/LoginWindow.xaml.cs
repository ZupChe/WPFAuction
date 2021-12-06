using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UserWpf.Model;
using UserWpf.ViewModel;

namespace UserWpf.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(MainWindow mainModel)
        {
            InitializeComponent();
            var model = new LoginWindowViewModel((sender, args) =>
            args.Password = UserPass.Password);
            model.Ok += (sender, args) =>
            {
                (mainModel.DataContext as MainWindowViewModel).LoginUser = args.user;
                mainModel.loginBtn.Content = "Logout";
                if(args.user.IsAdmin)
                {
                    mainModel.newBtn.Visibility = Visibility.Visible;
                    mainModel.deleteBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    mainModel.bidBtn.Visibility = Visibility.Visible;
                }

                mainModel.lblwlc.Visibility = Visibility.Visible;                

                this.Close();
            };

            model.Error += (sender, args) =>
            {
                MessageBox.Show("Login failed. Please try again");
            };

            this.DataContext = model;
        }

       
    }
}
