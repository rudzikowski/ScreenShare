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
using System.Text.RegularExpressions;

namespace prakt_ScreenShare.View
{
    /// <summary>
    /// Logika interakcji dla klasy IP_Form.xaml
    /// </summary>
    public partial class IP_Form : Window
    {
        bool type;
        public IP_Form(bool _type)//true => serwer    false => klient
        {
            InitializeComponent();
            type = _type;
            if(_type == true)
            {
                title_label.Content = "Podaj adres IP klienta";
            }
            else
            {
                title_label.Content = "Podaj adres IP serwera";
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string IP = $"{IP_1.Text}.{IP_2.Text}.{IP_3.Text}.{IP_4.Text}";
            //title_label.Content = IP;
           if(type == true)
            {
                ServerWindow servPage = new ServerWindow(IP);
                servPage.Show();
                this.Close();
            }
            else
            {
                ClientWindow clientPage = new ClientWindow(IP);
                clientPage.Show();
                this.Close();
            }
           
        }
    }
}
