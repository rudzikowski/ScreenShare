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
using System.Diagnostics;
using prakt_ScreenShare.ViewModel;

namespace prakt_ScreenShare.View
{
    /// <summary>
    /// Logika interakcji dla klasy ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        bool isdoing = false;
        int Port;
        ClientWindowViewModel viewmodel;
        public ClientWindow()
        {
            InitializeComponent();
            viewmodel = new ClientWindowViewModel();
            DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var th = new Thread(StartClient);
            if (isdoing == false)
            {

                if (Port_textbox.Text == "")
                {
                    MessageBox.Show("Nie podano portu");
                }
                else
                {
                    btn_start.Content = "Stop";
                    btn_start.Background = new SolidColorBrush(Colors.Crimson);
                    Port = int.Parse(Port_textbox.Text);
                    ComboBox_server.IsEnabled = false;
                    Port_textbox.IsEnabled = false;
                    isdoing = true;
                    th.Start();
                }

            }
            else
            {
                btn_start.Content = "Start";
                btn_start.Background = new SolidColorBrush(Colors.Green);
                ComboBox_server.IsEnabled = true;
                Port_textbox.IsEnabled = true;
                isdoing = false;
                //th.Abort();
            }

        }
        public void StartClient()
        {
            IPAddress ipAddress = IPAddress.Parse(viewmodel.User.IP);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);
            try
            {
                while (isdoing)
                {

                    byte[] bytes = new byte[1024];
                    Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    sender.Connect(remoteEP);

                    try
                    {
                        Debug.WriteLine("Socket connected");
                        byte[] msg = CaptureMyScreen();
                        int bytesSent = sender.Send(msg);
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
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    Thread.Sleep(17);
                }
            }
            catch
            {
                MessageBox.Show("Nie udało się nazwiązać połączenia");
            }

        }
        public byte[] CaptureMyScreen()
        {
            Bitmap bitmap;
            MemoryStream ms = new MemoryStream();
            bitmap = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }
            ms = GetCompressedBitmap(bitmap, 90L);
            Debug.WriteLine(ms.ToArray().Length);
            byte[] bitmapData = ms.ToArray();
            return bitmapData;
        }
        private MemoryStream GetCompressedBitmap(Bitmap bmp, long quality)
        {
            using (var mss = new MemoryStream())
            {
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                ImageCodecInfo imageCodec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(o => o.FormatID == ImageFormat.Jpeg.Guid);
                EncoderParameters parameters = new EncoderParameters(1);
                parameters.Param[0] = qualityParam;
                bmp.Save(mss, imageCodec, parameters);
                return mss;
            }
        }
    }
}