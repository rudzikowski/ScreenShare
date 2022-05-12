using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace prakt_ScreenShare.ViewModel
{
    public class ServerWindowViewModel : ViewModelBase
    {
        ImageSource imagesource;
        public ImageSource _ImageSource { get { return imagesource; } set { imagesource = value; OnPropertyChanged("_ImageSource"); } }

    }
}
