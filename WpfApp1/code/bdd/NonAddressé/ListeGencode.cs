using System.Collections.Generic;
using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.code.bdd.NonAddressé
{
    class ListeGencode
    {

        public List<NA2> _NaMC;
        public Bdd bd;
        public ListeGencode()
        {
            bd = Bdd.Instance();
            _NaMC = new List<NA2>();
            var _mc = Bdd.Instance().ListeNA();
        }
        public static int Mtri(NA2 x, NA2 y)
        {
            int i = int.Parse(x.loc.Split('.')[0]);
            int j = int.Parse(y.loc.Split('.')[0]);

            if (i > j)
            {
                return 1;
            }
            else if (i < j)
            {
                return -1;
            }
            else
            {
                int i2 = int.Parse(x.loc.Split('.')[1]);
                int j2 = int.Parse(y.loc.Split('.')[1]);

                if (i2 > j2)
                {
                    return 1;
                }
                else if (i2 < j2)
                {
                    return -1;
                }
                return 0;
            }
        }
        //todo changer nom
        public List<NA2> TriDesFamilles(List<NA2> Atrier)
        {
            List<NA2> NonTrier = new List<NA2>();
            foreach (NA2 nA in Atrier)
            {
                List<NonAddresseS> n = bd.SearchLocProduit(long.Parse(nA.Ean));
                if (n.Count != 0)
                {
                    nA.loc = "";
                    foreach (NonAddresseS nonAddresseS in n)
                    {
                        nA.loc = " " + nonAddresseS.Alle + "." + nonAddresseS.Trave;
                    }
                    this._NaMC.Add(nA);
                }
                else
                {
                    NonTrier.Add(nA);
                }
            }
            _NaMC.Sort(Mtri);
            return NonTrier;
        }
    }
}
