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
        public LoginWindow()
        {
            InitializeComponent();
            var model = new LoginWindowViewModel((sender, args) =>
    args.Password = UserPass.Password);
            model.Ok += (sender, args) =>
            {
                MainWindow main = new MainWindow();
                main.Show();
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
