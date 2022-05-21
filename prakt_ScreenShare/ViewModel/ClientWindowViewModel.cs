using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prakt_ScreenShare.Model;
using prakt_ScreenShare.Services;

namespace prakt_ScreenShare.ViewModel
{
    public class ClientWindowViewModel : ViewModelBase
    {
        DataBaseService db;
        List<UserEntries> users;
        public List<UserEntries> Users { get { return users; } set { users = value;OnPropertyChanged("Users"); } }
        UserEntries user;
        public UserEntries User { get { return user; } set { user = value;OnPropertyChanged("User"); } }
        public ClientWindowViewModel()
        {
            users = new List<UserEntries>();
            db = new DataBaseService();
            Refresh();
        }
        public async void Refresh()
        {
            users = await db.GetUsers();
        }
    }
}
