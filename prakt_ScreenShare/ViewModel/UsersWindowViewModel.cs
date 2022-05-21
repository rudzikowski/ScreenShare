using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prakt_ScreenShare.Model;
using prakt_ScreenShare.Services;
namespace prakt_ScreenShare.ViewModel
{
    public class UsersWindowViewModel : ViewModelBase
    {

        DataBaseService db = new DataBaseService();
        List<UserEntries> _users;
        public List<UserEntries> Users { get { return _users; } set { _users = value; OnPropertyChanged("Users"); } }
        UserEntries _user;
        public UserEntries User { get { return _user; } set { _user = value; OnPropertyChanged("User"); } }
        public UsersWindowViewModel()
        {
            _users = new List<UserEntries>();
            _user = new UserEntries();
            Refresh();
        }
        public async void Refresh()
        {
            Users = await db.GetUsers();
            Debug.WriteLine("Refreshed");
        }
        public void DeleteUser()
        {
            db.DeleteUser(_user);
            Refresh();
        }
    }
}
