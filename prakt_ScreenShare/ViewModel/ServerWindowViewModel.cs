using prakt_ScreenShare.Model;
using prakt_ScreenShare.Services;
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
        DataBaseService db;
        ImageSource imagesource;
        public ImageSource _ImageSource { get { return imagesource; } set { imagesource = value; OnPropertyChanged("_ImageSource"); } }
        List<UserEntries> users;
        public List<UserEntries> Users { get { return users; } set { users = value; OnPropertyChanged("Users"); } }
        UserEntries user;
        public UserEntries User { get { return user; } set { user = value; OnPropertyChanged("User"); } }
        public ServerWindowViewModel()
        {
            db = new DataBaseService();
            users = new List<UserEntries>();
            user = new UserEntries();
            Refresh();
        }
        public async void Refresh()
        {
            Users = await db.GetUsers();
            UserEntries Any = new UserEntries() { Name = "Any",IP = ""};
            Users.Add(Any);
        }
    }
}
