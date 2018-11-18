using SQLite;

namespace WpfApp1.code.bdd
{
    [Table("NA")]
    class NAbdd
    {

            [PrimaryKey, AutoIncrement]
            public int _Id { get; set; }
            [Column("test")]
            public string _LastName { get; set; }
            [Column("motcomplet")]
            public bool _motcomplet { get; set; }
            [Column("secteur")]
            public int _RoleId { get; set; }     
    }
}
