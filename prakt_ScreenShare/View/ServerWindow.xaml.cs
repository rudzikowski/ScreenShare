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
        bool isdoing;
        ServerWindowViewModel viewModel = new ServerWindowViewModel();
        public ServerWindow()
        { 
            DataContext = viewModel;
            InitializeComponent();
        }

        private void start_click(object sender, RoutedEventArgs e)
        {
            start_btn.IsEnabled = false;
            stop_btn.IsEnabled = true;
            isdoing = true;
            var th = new Thread(StartServer);
            Debug.WriteLine("klik");
            th.Start();
        }

        private void stop_click(object sender, RoutedEventArgs e)
        {
            start_btn.IsEnabled = true;
            stop_btn.IsEnabled = false;
            isdoing = false;
        }
        public void StartServer()
        {
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8080);
                // Create a Socket that will use Tcp protocol
                Socket listener = new Socket(localEndPoint.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method
                listener.Bind(localEndPoint);
                Socket handler;
            while (isdoing)
            {
                // Specify how many requests a Socket can listen before it gives Server busy response.
                // We will listen 10 requests at a time
                listener.Listen(10);
                Debug.WriteLine("Waiting for a connection...");
                handler = listener.Accept();

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
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        listener.Shutdown(SocketShutdown.Both);
                        listener.Close();
        }
    }
}
