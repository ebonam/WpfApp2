using SQLite;
using System;
using System.Collections.Generic;
using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.code.bdd
{
    class Bdd : IDisposable
    {
        SQLiteConnection conn;
        private static Bdd _instance = null;

        private Bdd()
        {
            string _dbPath = "carrefour.db3";
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
            conn.CreateTable<NA>();
            conn.CreateTable<NonAddresseS>();
        }
        public void AddNA(NA mc)
        {
            conn.Insert(mc);
        }
        public void AddNA(string Nom, int rayon, bool MC, string secteur)
        {
           NA mc = new NA();
            mc.Setter(Nom, rayon, MC, secteur);
            Console.WriteLine(mc.ToString());
            AddNA(mc);
        }



        public List<NA> ListeNA()
        {
            List<NA> listena = conn.Query<NA>("SELECT * FROM NA; ");
            return listena;
        }
        public List<NA> ListeNA(string sec)
        {
            List<NA> roles = conn.Table<NA>().Where(x => x._sec == sec).ToList();
            return roles;
        }

        public void ModifNA(ListeArticle lA)
        {
            conn.Update(lA);
        }
        public void RemoveNA(NA i)
        {

            conn.Delete(i);

        }

        public void AddProduit(long codebar, string lib, int alle, int trave)
        {
            //conn.q("            SELECT name FROM sqlite_master WHERE type IN('table', 'view') AND name NOT LIKE 'sqlite_%' ORDER BY 1");
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
            catch (Exception e)
            {
                Console.WriteLine(lA.Ean);
                Console.WriteLine(e.Message);
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

        public List<NonAddresseS> SearchLocProduit(long produ)
        {
            List<NonAddresseS> roles = conn.Table<NonAddresseS>().Where(x => x.Ean == produ).ToList();
            return roles;
        }

        public void ViderTProduit()
        {
            conn.DropTable<ListeArticle>();
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
