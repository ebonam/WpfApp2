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
        SQLiteConnection conn;

        private Bdd()
        {



            // Récupération du chemin vers notre fichier de base de données
            string _dbPath = "myDb.db3";
            // Instanciation de notre connexion
           
        conn = new SQLiteConnection(_dbPath);
            createTable();
        }

        private static Bdd _instance = null;

        public static Bdd Instance() {
            if (_instance == null)
                _instance = new Bdd();
            return _instance;
        }
      


        public void createTable() {
            conn.CreateTable<NAbdd>();

        }
        public void addNA( NAbdd nAbdd) {
            conn.Insert(nAbdd);
        }
        public void addNA(string Nom, int rayon, bool MC,int secteur)
        {
            NAbdd nAbdd = new NAbdd();
            nAbdd.NAbdd1(Nom, rayon, MC, secteur);
            Console.WriteLine(nAbdd.ToString());
            addNA(nAbdd);
         }
        public List<NAbdd> listeNA() {
            List<NAbdd> listena = conn.Query<NAbdd>("SELECT * FROM NA ");

            foreach (NAbdd n in listena){

                Console.WriteLine(n.ToString());
            }
            Console.Write("\n ");
            return listena;
        }
        public void ModifNA(NAbdd na) {
            conn.Update(na);

        }


        public void addProduit() {
            //@todo
        }

        public string searchLocProduit() {


            return null;
        }

        public void ViderTProduit()
        {

            //conn.DropTable<Produit>();
           
        }




        public void Test()
        {
            NAbdd r1 = new NAbdd() {  };
            conn.Insert(r1);
            // Des méthodes similaires existent pour les opérations Update et Delete
           // List<NAbdd> roles = conn.Table<NAbdd>().Where(x => x._LastName == "Administrator").ToList();
         //   IEnumerable<NAbdd> personnes = conn.Query<NAbdd>("SELECT * FROM People WHERE RoleId = {0}", r1._Id);
            conn.Query<NAbdd>("");
        }
        public void SelectGencodetoloc() {

        }

       
    }
}
