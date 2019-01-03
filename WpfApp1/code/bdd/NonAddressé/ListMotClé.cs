using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WpfApp1.code.bdd.NonAddressé
{
    class ListMotClé
    {

        public List<NA> _NaMC;
        public Bdd bd;
        List<MotCle> _mc = new List<MotCle>();
        public ListMotClé()
        {
            bd = Bdd.Instance();
            _NaMC = new List<NA>();
            var _mc = Bdd.Instance().ListeNA();
        }
        public List<NA> TriDesFamilles(List<NA> Atrier)
        {
            List<NA> NonTrier = new List<NA>();
            // List<NA> nAbdds = new List<NA>();

            foreach (NA nA in Atrier)
            {
                bool flag = false;
                int i = 0;
                while (i < _mc.Count && !flag)
                {
                    MotCle mc = _mc[i];
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
                        nA.loc = "" + mc._Rayon;
                        flag = true;
                        _NaMC.Add(nA);
                    }
                }
                if (!flag)
                {
                    NonTrier.Add(nA);
                }
            }
            return NonTrier;
        }
    }
}
