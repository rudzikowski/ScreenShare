using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Logika interakcji dla klasy ServerWindow.xaml
    /// </summary>
    public partial class ServerWindow : Window
    {

        bool isdoing = false;
        ServerWindowViewModel viewModel = new ServerWindowViewModel();
        int port;
        public ServerWindow()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void start_click(object sender, RoutedEventArgs e)
        {
            if (isdoing == false)
            {
                
                if (Port.Text == "")
                {
                    MessageBox.Show("Nie podano portu");
                }
                else if (viewModel.User.Name == null)
                {
                    MessageBox.Show("Nie wybrano klienta");
                }
                else
                {
                    start_btn.Content = "Stop";
                    isdoing = true;
                    port = int.Parse(Port.Text);
                    Port.IsEnabled = false;
                    ComboBox_server.IsEnabled = false;
                    isdoing = true;
                    var th = new Thread(StartServer);
                    Debug.WriteLine("klik");
                    th.Start();
                }
            }
            else
            {
                start_btn.Content = "Start";
                isdoing = false;
                ComboBox_server.IsEnabled = true;
                Port.IsEnabled = true;
            }

        }
        public void StartServer()
        {
            IPAddress IP;
            if (viewModel.User.Name == "Any")
            {
                IP = IPAddress.Any;
            }
            else
            {
                IP = IPAddress.Parse(viewModel.User.IP);
            }
            IPEndPoint localEndPoint = new IPEndPoint(IP, port);
            // Create a Socket that will use Tcp protocol
            Socket listener = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            // A Socket must be associated with an endpoint using the Bind method
            listener.Bind(localEndPoint);
            Socket handler;
            listener.Listen(10);
            Debug.WriteLine("Waiting for a connection...");
            handler = listener.Accept();
            while (isdoing)
            {
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a

                // Incoming data from the client.

                int dataSize = 0;
                dataSize = 0;
                byte[] b = new byte[1024 * 10000];  //Picture of great
                dataSize = handler.Receive(b);
                if (dataSize > 0)
                {
                    using (MemoryStream stream = new MemoryStream(b))
                    {
                        //Debug.WriteLine("Tutaj");
                        viewModel._ImageSource = BitmapFrame.Create(stream,
                                                          BitmapCreateOptions.None,
                                                          BitmapCacheOption.OnLoad);
                    }



                    //Debug.WriteLine("Text received : {0}", data);

                    //byte[] msg = Encoding.ASCII.GetBytes(data);
                    //handler.Send(msg);
                }

            }
            handler = null;
            Debug.WriteLine("Tutaj");
            //handler.Shutdown(SocketShutdown.Both);
            listener.Shutdown(SocketShutdown.Both);
            listener.Close();
        }
    }
}
