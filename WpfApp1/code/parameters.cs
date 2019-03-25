using System;
using System.Collections.Generic;

namespace WpfApp1.code
{
    [Serializable]
    public class Parameters
    {
        public class BLAALBA
        {
            public string SetSec(int rayon)
            {
                LisSecteur s1 = new LisSecteur();
                foreach (Defrayon sec in s1.secteurs)
                {
                    if (sec.rayon.Contains("" + rayon))
                    {
                        return sec.nom;
                    }
                }
                return "NA";

            }
        }

        public int SortRayon(object A, object B)
        {
            string s = "", v = "";
            int cpr = s.CompareTo(v);
            if (cpr == 0)
            {
                int i = 0, j = 0;
                cpr = i.CompareTo(j);
                if (cpr == 0)
                {
                    int i2 = 0, j2 = 0;
                    cpr = i2.CompareTo(j2);
                }
            }
            return cpr;
        }

        public class Emag
        {
            public int EAN, LIB, QTE, PRIX, LOC;
        }

        public class Prod
        {
            public int EAN, LIB, Trave, Alle;
        }
        public class LisSecteur
        {
            public LisSecteur()
            {
                secteurs = new List<Defrayon>();
                nomSecteur = new List<string>();
            }
            public void Add(string str)
            {
                Defrayon dr = new Defrayon(str);
                
                secteurs.Add(dr);
                nomSecteur.Add(str);
            }
            public void Remov(int selectedIndex)
            {
                secteurs.RemoveAt(selectedIndex);
                nomSecteur.RemoveAt(selectedIndex);
            }
            public void AddRayon(string sec, string rayon)
            {
                var ds = secteurs.Find(x => x.nom == sec);
                ds.rayon.Add(rayon);
            }
            public List<string> GetRayon(string str)
            {
                return secteurs.Find(x => x.nom == str).rayon;

            }
            public void DeRayon(int str, string sec)
            {
                var ds = secteurs.Find(x => x.nom == sec);
                ds.rayon.RemoveAt(str);

            }
            public List<Defrayon> secteurs;
            public List<string> nomSecteur;
        }
        public class Defrayon
        {
            public Defrayon() { }
            public string nom;
            public List<string> rayon;
            public Defrayon(string nom) {
                this.nom = nom;
                rayon = new List<string>();
            }
        }

        public class TG
        {
            public List<string> tgs;
            public List<Doubleint> emplacement;
            public TG()
            {
                tgs = new List<string>();
                emplacement = new List<Doubleint>();
            }
            [Serializable]
            public class Doubleint
            {
                public int rayon, trave;
                public string sec;
                public Doubleint() { }

                public Doubleint(int r, int tr, string sec)
                {
                    this.rayon = r;
                    this.trave = tr;
                    this.sec = sec;
                }

                public string Tolist()
                {
                    return rayon + "." + trave + "   " + sec;

                }

            }

            public void Ajout(int text1, int text2, string sec)
            {
                var di = new Doubleint(text1,text2, sec);
                emplacement.Add(di);
                tgs.Add(di.Tolist());
            }

            public void Remov(int selectedIndex)
            {
                emplacement.RemoveAt(selectedIndex);
                tgs.RemoveAt(selectedIndex);
            }
        }
        public Emag emag;
        public Prod prod;
        public LisSecteur ps;
        public bool vlepprintdirect;
        public bool Emagprintdirect;
        public bool Naprintdirect;
        public TG TGs;
        public string datelastUpdateBase;

        private static Parameters _instance = null;

        public void Sav()
        {
            Serializator serializator = new Serializator() { };
            serializator.SerializeObject<Parameters>(this, "objet.xml");

        }


        public void Charge()
        {
            Serializator serializator = new Serializator() { };
            _instance = serializator.DeSerializeObject<Parameters>("objet.xml");

        }


        private Parameters()
        {
            this.prod = new Prod();
            this.TGs = new TG();
            this.ps = new LisSecteur();
            this.emag = new Emag();
        }

        public static Parameters Instance()
        {
            if (_instance == null)
            {

                _instance = new Parameters();
                Serializator serializator = new Serializator();
                _instance = serializator.DeSerializeObject<Parameters>("objet.xml");
            }
            return _instance;
        }
    }
}
