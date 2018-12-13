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
            nAbdd.setter(Nom, rayon, MC, secteur);
            Console.WriteLine(nAbdd.ToString());
            addNA(nAbdd);
         }
        public List<NAbdd> listeNA() {
            List<NAbdd> listena = conn.Query<NAbdd>("SELECT * FROM NA ");

          /*  foreach (NAbdd n in listena){

                Console.WriteLine(n.ToString());
            }
            Console.Write("\n ");*/
            return listena;
        }
        public void ModifNA(ListeArticle lA) {
            conn.Update(lA);
        }

        public void addProduit(long codebar, string lib, int alle, int trave) {
            ListeArticle lA = new ListeArticle();
            lA.setter(codebar, lib, alle, trave);
            addProduit(lA);
        }

        private void addProduit(ListeArticle lA)
        {
            conn.Insert(lA);
        }

        public List<ListeArticle> searchLocProduit(long produ) {

            List<ListeArticle> roles = conn.Table<ListeArticle>().Where(x => x._codebar == produ).ToList();
            return roles;
        }

        public void ViderTProduit()
        {

            conn.DropTable<ListeArticle>();
           
        }

        /**not used 
         * reference & test*/

        public void Test()
        {
            NAbdd r1 = new NAbdd() {  };
            conn.Insert(r1);
            // Des méthodes similaires existent pour les opérations Update et Delete
           // List<NAbdd> roles = conn.Table<NAbdd>().Where(x => x._LastName == "Administrator").ToList();
         //   IEnumerable<NAbdd> personnes = conn.Query<NAbdd>("SELECT * FROM People WHERE RoleId = {0}", r1._Id);
            conn.Query<NAbdd>("");
        }
       

       
    }
}
