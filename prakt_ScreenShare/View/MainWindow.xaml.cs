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

namespace prakt_ScreenShare.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServerWindow server = new ServerWindow();
            server.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClientWindow client = new ClientWindow();
            client.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UsersWindow userwindow = new UsersWindow();
            userwindow.ShowDialog();
        }

        private void Info_button_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ScreenShare \nAutor: Artur Rudzik\nWersja: 1.0", "Informacje o programie",MessageBoxButton.OK,MessageBoxImage.Information);
        }
    }
}
