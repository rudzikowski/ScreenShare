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
                    IPAddress ipAddress = IPAddress.Parse(IP);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);

                    
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
        public byte[] CaptureMyScreen()
        {
            Bitmap bitmap;
            MemoryStream ms = new MemoryStream();
            bitmap = new Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
           
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            }
           /*ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/bmp");
            myEncoder = System.Drawing.Imaging.Encoder.Compression;
            myEncoderParameters = new EncoderParameters(1);

            myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 10L);
            myEncoderParameters.Param[0] = myEncoderParameter;*/
            ms = GetCompressedBitmap(bitmap, 50L);
            Debug.WriteLine(ms.ToArray().Length);
            byte[] bitmapData = ms.ToArray();
            return bitmapData;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            isdoing = false;
            btn_start.IsEnabled = true;
            btn_stop.IsEnabled = false;
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
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