using SQLite;

namespace WpfApp1.code.bdd
{
    [Table("NA")]
    class NAbdd
    {
        //undone: plus besoin 
        public void Setter(string Nom, int Rayon, bool motcomplet, int sec)
        {
            _Nom = Nom;
            _Rayon = Rayon;
            _motcomplet = motcomplet;
            _sec = sec;
        }

        public override string ToString()
        {
            return "" + _Nom + "  " + _Rayon + "   " + _motcomplet + "   " + _sec + "\n";
        }

        [PrimaryKey, AutoIncrement]
        public int _Id { get; set; }
        [Column("Nom")]
        public string _Nom { get; set; }
        [Column("Rayon")]
        public int _Rayon { get; set; }
        [Column("boolComplet")]
        public bool _motcomplet { get; set; }
        [Column("secteur")]
        public int _sec { get; set; }
    }
}
