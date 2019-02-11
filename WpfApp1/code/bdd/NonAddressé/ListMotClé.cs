using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WpfApp1.code.bdd.NonAddressé
{
    class ListMotClé
    {

        public List<NA2> _NaMC;
        public Bdd bd;
        List<NA> _mc = new List<NA>();
        public ListMotClé(string text)
        {
            bd = Bdd.Instance();
            bd = Bdd.Instance();
            _NaMC = new List<NA2>();
            _mc = Bdd.Instance().ListeNA(text);
        }
        public List<NA2> TriDesFamilles(List<NA2> Atrier)
        {
            List<NA2> NonTrier = new List<NA2>();
            // List<NA> nAbdds = new List<NA>();

            foreach (NA2 nA in Atrier)
            {
                bool flag = false;
                int i = 0;
                while (i < _mc.Count && !flag)
                {
                    NA mc = _mc[i];
                    MatchCollection gege;
                    if (mc._motcomplet)
                    {
                        gege = Regex.Matches(nA.Lib, "( " + mc._Nom + " )|(^" + mc._Nom + ")|(" + mc._Nom + "$)");
                    }
                    else
                    {
                        gege = Regex.Matches(nA.Lib, mc._Nom);
                    }

                    if (gege.Count == 0)
                    {
                        i++;
                    }
                    else
                    {
                        nA.rayon = mc._Rayon;
                        
                         nA.loc="" + mc._Nom;
                        flag = true;
                        _NaMC.Add(nA);
                    }
                }
                if (!flag)
                {
                    NonTrier.Add(nA);
                }
            }
            _NaMC.Sort(Mtri);
            return NonTrier;
        }


        public static int Mtri(NA2 x, NA2 y)
        {
            int i = x.rayon;
            int j = y.rayon;
            int cpr = i.CompareTo(j);

            if (cpr == 0)
            {
                string i1 = x.loc;
                string j1 = y.loc;
                int cpr2 = i.CompareTo(j);
                return cpr2;
            }
                return cpr;
        }
    }
}
