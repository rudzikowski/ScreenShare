using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace prakt_ScreenShare.View
{
    /// <summary>
    /// Logika interakcji dla klasy ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        bool isdoing = true;
        string IP;
        public ClientWindow(string _IP)
        {
            InitializeComponent();
            IP = _IP;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            btn_start.IsEnabled = false;
            btn_stop.IsEnabled = true;
            isdoing = true;
            var th = new Thread(StartClient);
            th.Start();
        }
        public void StartClient()
        {
            while (isdoing)
            {
                byte[] bytes = new byte[1024];
                    // Connect to a Remote server
                    // Get Host IP Address that is used to establish a connection
                    // In this case, we get one IP address of localhost that is IP : 127.0.0.1
                    // If a host has multiple addresses, you will get a list of addresses
                    //IPHostEntry host = Dns.GetHostEntry("localhost");
                    //IPAddress ipAddress = host.AddressList[0];
                    IPAddress ipAddress = IPAddress.Parse(IP);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                    // Create a TCP/IP  socket.
                    Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                    // Connect the socket to the remote endpoint. Catch any errors.
                    try
                    {
                        // Connect to Remote EndPoint
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                        // Encode the data string into a byte array.
                        byte[] msg = CaptureMyScreen();

                        // Send the data through the socket.
                        int bytesSent = sender.Send(msg);

                        // Receive the response from the remote device.
                        //int bytesRec = sender.Receive(bytes);
                        //Console.WriteLine("Echoed test = {0}",
                        //Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        // Release the socket.
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }
                Thread.Sleep(1);
                }    
            }
        public byte[] CaptureMyScreen()
        {
            Bitmap bitmap;

            bitmap = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
           
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Bmp);
            byte[] bitmapData = ms.ToArray();
            return bitmapData;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            isdoing = false;
            btn_start.IsEnabled = true;
            btn_stop.IsEnabled = false;
        }
    }
}