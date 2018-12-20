using SQLite;
using System;
using System.Collections.Generic;
using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.code.bdd
{
    class Bdd
    {
        SQLiteConnection conn;
        private static Bdd _instance = null;

        private Bdd()
        {
            string _dbPath = "myDb.db3";
            conn = new SQLiteConnection(_dbPath);
            CreateTable();
        }

        public static Bdd Instance()
        {
            if (_instance == null)
                _instance = new Bdd();
            return _instance;
        }

        public void CreateTable()
        {
            conn.CreateTable<NAbdd>();
            conn.CreateTable<NonAddresseS>();
        }
        public void AddNA(NAbdd nAbdd)
        {
            conn.Insert(nAbdd);
        }
        public void AddNA(string Nom, int rayon, bool MC, int secteur)
        {
            NAbdd nAbdd = new NAbdd();
            nAbdd.Setter(Nom, rayon, MC, secteur);
            Console.WriteLine(nAbdd.ToString());
            AddNA(nAbdd);
        }
        public List<NAbdd> ListeNA()
        {
            List<NAbdd> listena = conn.Query<NAbdd>("SELECT * FROM NA ");

            return listena;
        }
        public void ModifNA(ListeArticle lA)
        {
            conn.Update(lA);
        }

        public void AddProduit(long codebar, string lib, int alle, int trave)
        {
            NonAddresseS lA = new NonAddresseS();
            lA.Setter(codebar, lib, alle, trave);
            AddProduit(lA);
        }

        public void AddProduit(NonAddresseS lA)
        {
            try
            {
                conn.Insert(lA);
            }
            catch (SQLite.SQLiteException) {
                Console.WriteLine(lA.Ean);
            }

        }


        public void AddProduit2(long codebar, string lib, int alle, int trave)
        {
            ListeArticle lA = new ListeArticle();
            lA.Setter(codebar, lib, alle, trave);
            AddProduit2(lA);
        }


        public void AddProduit2(ListeArticle lA)
        {
            conn.Insert(lA);
        }

        public List<ListeArticle> SearchLocProduit(long produ)
        {
            List<ListeArticle> roles = conn.Table<ListeArticle>().Where(x => x._codebar == produ).ToList();
            return roles;
        }

        public void ViderTProduit()
        {
            conn.DropTable<ListeArticle>();
        }
    }
}
