using System;
using SQLite;

namespace WpfApp1.code.bdd.NonAddresse
{
    public class NonAddresseS
    {
        [PrimaryKey, AutoIncrement]
        public int Id{ get; set; }
        [PrimaryKey]
        public long Ean { get; set; }
        public string Lib { get; set; }
        public  int Alle { get; set; }
        public int Trave { get; set; }

       public void Setter(long codebar, string lib, int alle, int trave)
        {
            Ean = codebar;
            Lib = lib;
            Alle = alle;
            Trave = trave;
        }
    }
}