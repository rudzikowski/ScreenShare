﻿using System;
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
        Socket handler;
        public ServerWindow()
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        private void start_click(object sender, RoutedEventArgs e)
        {
            var th = new Thread(StartServer);
            if (isdoing == false)
            {
                
                if (Port.Text == "")
                {
                    MessageBox.Show("Nie podano portu");
                }
                else
                {
                    start_btn.Content = "Stop";
                    isdoing = true;
                    port = int.Parse(Port.Text);
                    Port.IsEnabled = false;
                    //ComboBox_server.IsEnabled = false;
                    isdoing = true;
                    Debug.WriteLine("klik");
                    th.Start();
                }
            }
            else
            {
                start_btn.Content = "Start";
                isdoing = false;
                Port.IsEnabled = true;
            }

        }
        void StartServer()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            Socket listener = new Socket(localEndPoint.AddressFamily ,SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            while (isdoing)
            {
                listener.Listen(10);
                Debug.WriteLine("Waiting for a connection...");
                handler = listener.Accept();
                Debug.WriteLine("Connection...Accepted");
                int dataSize = 0;
                dataSize = 0;
                byte[] b = new byte[1024 * 10000];  //Picture of great
                dataSize = handler.Receive(b);
                if (dataSize > 0)
                {
                    using (MemoryStream stream = new MemoryStream(b))
                    {
                        viewModel._ImageSource = BitmapFrame.Create(stream,BitmapCreateOptions.None,BitmapCacheOption.OnLoad);
                    }
                }
            }
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            Debug.WriteLine("Zatrzymano");
        }
    }
}
