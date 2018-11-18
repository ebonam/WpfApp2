namespace WpfApp1.code.bdd.cmdVlep
{
    public class ProductVlep
    {
        private string gencode;
        private string prix1;
        private string prix2;
        private string qte;
        private string lib;

        public string Gencode { get => gencode; set => gencode = value; }
        public string Prix1 { get => prix1; set => prix1 = value; }
        public string Prix2 { get => prix2; set => prix2 = value; }
        public string Qte { get => qte; set => qte = value; }
        public string Lib { get => lib; set => lib = value; }

        public ProductVlep(string gencode, string prix1, string prix2, string qte, string lib)
        {
            this.Gencode = gencode;
            this.Prix1 = prix1;
            this.Prix2 = prix2;
            this.Qte = qte;
            this.Lib = lib;
        }
        public string  oString()
        {
             return Gencode + "   " + lib + "   " + qte + "   " + prix1 + "   " + prix2;

        }
    }
}