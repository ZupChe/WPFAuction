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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserWpf.ViewModel;

namespace UserWpf.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            NewEditWindow editWindow = new NewEditWindow();
            var editmodel = new NewEditWindowViewModel();
            editmodel.Ok += (s, args) => 
            {
                editWindow.Close();
                ((MainWindowViewModel)DataContext).updateList();
            };
            editWindow.DataContext = editmodel;
            editWindow.ShowDialog();
        }

               
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bidBtn_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as MainWindowViewModel;
            model.CurrentItem.UpdatePrice(model.LoginUser.Id);
            model.updateList();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as MainWindowViewModel;
            if (model.LoginUser == null)
            {
                LoginWindow loginWindow = new LoginWindow(this);
                loginWindow.Show();
            }
            else
            {
                model.LoginUser = null;
                this.loginBtn.Content = "Login";
                this.bidBtn.Visibility = Visibility.Collapsed;
                this.newBtn.Visibility = Visibility.Collapsed;
                this.deleteBtn.Visibility = Visibility.Collapsed;
                this.lblwlc.Visibility = Visibility.Collapsed;
            }

           
        }
    }
}
