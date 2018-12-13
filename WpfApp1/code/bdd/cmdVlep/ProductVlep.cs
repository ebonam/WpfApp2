namespace WpfApp1.code.bdd.cmdVlep
{
    public class ProductVlep
    {
        private long gencode;
        private string prix1;
        private string prix2;
        private string qte;
        private string lib;
        private string loc;

        public long Gencode { get => gencode; set => gencode = value; }
        public string Prix1 { get => prix1; set => prix1 = value; }
        public string Prix2 { get => prix2; set => prix2 = value; }
        public string Qte { get => qte; set => qte = value; }
        public string Lib { get => lib; set => lib = value; }
        public string Loc { get => loc; set => loc = value; }

        public ProductVlep(long gencode, string prix1, string prix2, string qte, string lib)
        {
            this.Gencode = gencode;
            this.Prix1 = prix1;
            this.Prix2 =prix2;
            this.Qte =qte;
            this.Lib = lib;
            Loc = "NA";
        }
        public void Searchemplacement() {
         //   Bdd.Instance().searchLocProduit(this.gencode);
        }

        public string  OString()
        {
             return Gencode + "   " + lib + "   " + qte + "   " + prix1 + "   " + prix2;
        }
    }
}