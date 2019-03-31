using SQLite;

namespace WpfApp1.code.bdd.BaseProduit
{

    [Table("NonAddresseS2")]
#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    public class NonAddresseS2
#pragma warning restore CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
    {

        public NonAddresseS2()
        { }
        [PrimaryKey, Column("Ean")]
        public long Ean { get; set; }

        [Column("Lib")]
        public string Lib { get; set; }
        [Column("Alle")]
        public int Alle { get; set; }
        [Column("Trave")]
        public int Trave { get; set; }

        public override bool Equals(object obj)
        {
            NonAddresseS2 s = obj as NonAddresseS2;
            return s != null &&
                   Ean == s.Ean &&
                   Lib == s.Lib &&
                   Alle == s.Alle &&
                   Trave == s.Trave;
        }

        public void Setter(long codebar, string lib, int alle, int trave)
        {
            Ean = codebar;
            Lib = lib;
            Alle = alle;
            Trave = trave;
        }
    }
}
