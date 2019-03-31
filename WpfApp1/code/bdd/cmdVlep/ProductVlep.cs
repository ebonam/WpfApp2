using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.code.bdd.cmdVlep
{
    public class ProductVlep
    {
        public string nCommande;
        public int rayon;
        public int Alle;
        public long Gencode { get; set; }
        public string Prix1 { get; set; }
        public string Prix2 { get; set; }
        public string Sec { get; set; }
        public string Qte { get; set; }
        public string Lib { get; set; }
        public string Loc { get; set; }
        //todo finir ici
        public ProductVlep(long gencode, string prix1, string prix2, string qte, string lib)
        {
            this.Gencode = gencode;
            this.Prix1 = prix1;
            this.Prix2 =prix2;
            this.Qte =qte;
            this.Lib = lib;
             Searchemplacement();
        }
        public void Searchemplacement()
        {
            
            var lis = Bdd.Instance().SearchLocProduit(this.Gencode);
            if (lis.Count != 0)
            {
                rayon = lis[0].Alle;
                Alle= lis[0].Trave;

                Loc = lis[0].Alle + "." + lis[0].Trave + "\n"; ;
            }
            else { Loc = "NA";
                rayon = 0;
                Alle = 0;
            }
        }
        public string  OString()
        {
             return Gencode + "   " + Lib + "   " + Qte + "   " + Prix1 + "   " + Prix2;
        }
    }
}