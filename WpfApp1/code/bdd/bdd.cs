using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.code.bdd
{
    class Bdd
    {

        public void Df()
        {
            SQLiteConnection connection = new SQLiteConnection("myDb.db3");
            connection.CreateTable<NAbdd>();

        }
        public void Test(SQLiteConnection conn)
        {
            NAbdd r1 = new NAbdd() {  };
            conn.Insert(r1);
            // Des méthodes similaires existent pour les opérations Update et Delete
            List<NAbdd> roles = conn.Table<NAbdd>().Where(x => x._LastName == "Administrator").ToList();
            IEnumerable<NAbdd> personnes = conn.Query<NAbdd>("SELECT * FROM People WHERE RoleId = {0}", r1._Id);
        }
        public void SelectGencodetoloc() {

        }







    }
}
