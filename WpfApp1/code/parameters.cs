using System;
using System.Collections.Generic;

namespace WpfApp1.code
{
    [Serializable]
    public class Parameters
    {

        public class defrayon
        {
            public string nom;
            public int min;
            public int max;
            public bool paire;
            public List<string> rayon;

        }

        public class TG
        {
            public List<string> tgs;
            public List<doubleint> emplacement;
            public TG()
            {
                tgs = new List<string>();
                emplacement = new List<doubleint>();
            }

            public class doubleint
            {
                public int rayon, trave;
                public string sec;
                public doubleint() { }

                public doubleint(int r, int tr, string sec)
                {
                    this.rayon = r;
                    this.trave = tr;
                    this.sec = sec;
                }

                public string tolist()
                {
                    return rayon + "." + trave + "   " + sec;

                }

            }

            public void Ajout(string text1, string text2, string sec)
            {

                var di = new doubleint(int.Parse(text1), int.Parse(text2), sec);
                emplacement.Add(di);
                tgs.Add(di.tolist());

            }

            internal void Remov(int selectedIndex)
            {
                emplacement.RemoveAt(selectedIndex);
                tgs.RemoveAt(selectedIndex);
            }
        }

        public List<defrayon> ps;
        public bool vlepprintdirect;
        public bool Emagprintdirect;
        public bool Naprintdirect;
        public TG TGs;
        public string datelastUpdateBase;

        private static Parameters _instance = null;

        public void sav()
        {
            Serializator serializator = new Serializator() { };
            serializator.SerializeObject<Parameters>(this, "objet.xml");

        }


        public void charge()
        {
            Serializator serializator = new Serializator() { };
            _instance = serializator.DeSerializeObject<Parameters>("objet.xml");

        }


        private Parameters()
        {

        }

        public static Parameters Instance()
        {
            if (_instance == null)
            {
                Serializator serializator = new Serializator();
                _instance = serializator.DeSerializeObject<Parameters>("objet.xml");
            }
            return _instance;
        }
    }
}
