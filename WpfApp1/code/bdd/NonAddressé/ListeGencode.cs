using System.Collections.Generic;
using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.code.bdd.NonAddressé
{
    class ListeGencode
    {

        public List<NA> _NaMC;
        public Bdd bd;
        public ListeGencode()
        {
            bd = Bdd.Instance();
            _NaMC = new List<NA>();
            var _mc = Bdd.Instance().ListeNA();
        }
        public List<NA> TriDesFamilles(List<NA> Atrier)
        {
            List<NA> NonTrier = new List<NA>();
            foreach (NA nA in Atrier)
            {
                List<NonAddresseS> n = bd.SearchLocProduit(nA.Ean);
                if (n.Count != 0)
                {
                    nA.loc = "";
                        foreach (NonAddresseS nonAddresseS in n)
                    {
                        nA.loc += " " + nonAddresseS.Alle + "."+nonAddresseS.Trave;
                    }
                    this._NaMC.Add(nA);
                }
                else
                {
                    NonTrier.Add(nA);
                }
            }
            return NonTrier;
        }
    }



}
}
