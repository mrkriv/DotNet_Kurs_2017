using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TelegaBot.Data
{
    [Serializable]
    public class DBContext
    {
        private const string DBFile = "database.dat";
        public List<TelegaUser> Users { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Schedule> Schedules { get; set; }

        public DBContext()
        {
            Schedules = new List<Schedule>();
            Users = new List<TelegaUser>();
            Lessons = new List<Lesson>();
        }

        public void Save()
        {
            var bf = new BinaryFormatter();
            using (var fs = File.OpenWrite(DBFile))
            {
                bf.Serialize(fs, this);
            }
        }

        public static DBContext Load()
        {
            if (!File.Exists(DBFile))
                return new DBContext();

            var bf = new BinaryFormatter();
            using (var fs = File.OpenRead(DBFile))
            {
                return (DBContext) bf.Deserialize(fs);
            }
        }
    }
}