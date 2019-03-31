using System;
using SQLite;

namespace WpfApp1.code.bdd.NonAddresse
{
    [Table("NonAddresseS")]
     class NonAddresseS
    {

        public NonAddresseS()
        { }
        [PrimaryKey, AutoIncrement]
        public int? Id{ get; set; }
        [Column("Ean")]
        public long Ean { get; set; }
        
        [Column("Lib")]
        public string Lib { get; set; }
        [Column("Alle")]
        public  int Alle { get; set; }
        [Column("Trave")]
        public int Trave { get; set; }
        /*
        public string SetSec()
        {
            Parameters p = Parameters.Instance();
          
            foreach (Parameters.Defrayon sec in p.ps.secteurs)
            {
                if (sec.rayon.Contains("" + Alle))
                {

                    return sec.nom;
                }
            }
            return "NA";

        }
        */

        public void Setter(long codebar, string lib, int alle, int trave)
        {
            Ean = codebar;
            Lib = lib;
            Alle = alle;
            Trave = trave;
        }
    }
}