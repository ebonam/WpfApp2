using SQLite;

namespace WpfApp1.code.client
{
    [Table("Client")]
    public class ClientBdd
    {
            [PrimaryKey]
            public int IdClient { get; set; }
            [Column("Nom")]
            public string Nom { get; set; }
            [Column("Prenom")]
            public string Prenom { get; set; }
            [Column("Telephone1")]
            public string Telephone1 { get; set; }
            [Column("Telephone2")]
            public string Telephone2 { get; set; }      
    }
}