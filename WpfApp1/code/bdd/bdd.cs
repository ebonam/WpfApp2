using SQLite;
using System;
using System.Collections.Generic;
using WpfApp1.code.bdd.BaseProduit;
using WpfApp1.code.bdd.NonAddresse;
using WpfApp1.code.client;

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

        Parameters p = Parameters.Instance();
        public void CreateTable()
        {
            conn.CreateTable<NA>();
            conn.CreateTable<NonAddresseS>();
            conn.CreateTable<NonAddresseS2>();
            conn.CreateTable<ClientBdd>();
        }
        public List<ClientBdd> ListeClient(string idClient, string nom, string prenom)
        {

            if (idClient != "")
            {
                int i = int.Parse(idClient);

                if (nom != "")
                {

                    if (prenom != "")
                    {
                        return conn.Table<ClientBdd>().Where(x => x.IdClient == i && x.Nom == nom && x.Prenom == prenom).ToList();

                    }
                    else
                    {
                        return conn.Table<ClientBdd>().Where(x => x.IdClient == i && x.Nom == nom).ToList();
                    }
                }
                else
                {

                    if (prenom != "")
                    {
                        return conn.Table<ClientBdd>().Where(x => x.IdClient == i && x.Prenom == prenom).ToList();

                    }
                    else
                    {
                        return conn.Table<ClientBdd>().Where(x => x.IdClient == i).ToList();
                    }

                }

            }
            else
            {
                if (nom != "")
                {

                    if (prenom != "")
                    {
                        return conn.Table<ClientBdd>().Where(x => x.Nom == nom && x.Prenom == prenom).ToList();

                    }
                    else
                    {
                        return conn.Table<ClientBdd>().Where(x => x.Nom == nom).ToList();
                    }
                }
                else
                {

                    if (prenom != "")
                    {
                        return conn.Table<ClientBdd>().Where(x => x.Prenom == prenom).ToList();

                    }
                    else
                    {
                        return conn.Query<ClientBdd>("SELECT * FROM Client; ");
                    }

                }

            }
        }

        internal void Addclient(ClientBdd art)
        {
            conn.InsertOrReplace(art);
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

        public void VideNA()
        {
            conn.DeleteAll<NA>();
        }
        public void RemoveNA(NA i)
        {
            conn.Delete(i);
        }

        public void AddProduit(long codebar, string lib, int alle, int trave)
        {
            NonAddresseS lA = new NonAddresseS();
            lA.Setter(codebar, lib, alle, trave);
            AddProduit(lA);
        }

        internal int AddProduit(NonAddresseS2 nonAddresseS)
        {
            List<NonAddresseS2> roles = conn.Table<NonAddresseS2>().Where(x => x.Ean == nonAddresseS.Ean).ToList();
            if (roles.Count == 0)
            {
                conn.Insert(nonAddresseS);
                Console.WriteLine("ajouter" + nonAddresseS.Lib);
                return 1;
            }
            else if (!roles[0].Equals(nonAddresseS))
            {

                if (!p.TGs.Appartient(nonAddresseS))
                {
                    Console.WriteLine("Modifié" + nonAddresseS.Lib);

                    conn.Update(nonAddresseS);
                    return 2;
                }
                else
                {
                    Console.WriteLine("TG" + nonAddresseS.Lib);
                    return 3;
                }

            }
            else
            {
                Console.WriteLine("ignoré" + nonAddresseS.Lib);
                return 0;
            }
        }

        public void UpdateProduit(NonAddresseS non)
        {
            conn.Update(non);
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
        //todo
        public List<NonAddresseS2> SearchLocProduit(long produ)
        {
            List<NonAddresseS2> roles = conn.Table<NonAddresseS2>().Where(x => x.Ean == produ).ToList();
            return roles;
        }

        public void ViderTProduit()
        {
            conn.DeleteAll<ListeArticle>();
        }
        public void ViderClient()
        {
            conn.DeleteAll<ClientBdd>();
        }
        public void ViderNa()
        {
            conn.DeleteAll<ClientBdd>();
        }
        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
