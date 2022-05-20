using prakt_ScreenShare.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using prakt_ScreenShare.Model;
using System.Diagnostics;

namespace prakt_ScreenShare.View
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        DataBaseService db = new DataBaseService();
        public NewUserWindow()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Kliknięto zapisz
        {
            UserEntries user = new UserEntries() {Name = UserName_txt.Text, IP = $"{IP_1.Text}.{IP_2.Text}.{IP_3.Text}.{IP_4.Text}"}; //Obiekt User
            db.AddUser(user);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Kliknięto Anuluj
        {
            this.Close();
        }
    }
}
