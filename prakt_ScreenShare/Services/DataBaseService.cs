using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using prakt_ScreenShare.Model;
using prakt_ScreenShare.Services;
using SQLite;
using SQLitePCL;


namespace prakt_ScreenShare.Services
{
    public class DataBaseService
    {
        SQLiteConnection db;
        async Task Init()
        {
            if (db != null)
                return;
            var databasePath = App.databasePath;
            db = new SQLiteConnection(databasePath);
            db.CreateTable<UserEntries>();
        }
        public async void AddUser(UserEntries user)
        {
            await Init();
            db.Insert(user);
        }
        public async void DeleteUser(UserEntries user)
        {
            await Init();
            db.Delete(user);
        }
        public async Task<List<UserEntries>> GetUsers()
        {
            await Init();
            var query = db.Table<UserEntries>();
            return query.ToList();
        }

    }
}
