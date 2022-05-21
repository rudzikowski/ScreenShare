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
using prakt_ScreenShare.ViewModel;

namespace prakt_ScreenShare.View
{
    /// <summary>
    /// Logika interakcji dla klasy UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        UsersWindowViewModel viewmodel;
        public UsersWindow()
        {
            InitializeComponent();
            viewmodel = new UsersWindowViewModel();
            DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newuser = new NewUserWindow();
            newuser.Show();
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            viewmodel.Refresh();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            viewmodel.DeleteUser();
        }
    }
}
