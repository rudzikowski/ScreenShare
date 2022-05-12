using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using prakt_ScreenShare.View;

namespace prakt_ScreenShare.ViewModel
{
    public class IP_Form_ViewModel : ViewModelBase
    {
        private int IP1;
        private int IP2;
        private int IP3;
        private int IP4;
        public int IP1_ { get { return IP1; } set { IP1 = value; } }
        public int IP2_ { get { return IP2; } set { IP2 = value; } }
        public int IP3_ { get { return IP3; } set { IP3 = value; } }
        public int IP4_ { get { return IP4; } set { IP4 = value; } }
        public IP_Form_ViewModel()
        {

        }
    }
}
