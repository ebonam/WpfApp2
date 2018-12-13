using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.code.bdd
{
    class ListeArticle
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Column("codebar")]
        public long _codebar { get; set; }
        [Column("lib")]
        public string _lib { get; set; }
        [Column("alle")]
        public int _alle { get; set; }
        [Column("trave")]
        public int _trave { get; set; }

        public void setter(long codebar, string lib, int alle, int trave)
        {
            _codebar = codebar;
            _lib = lib;
            _alle = alle;
            _trave = trave;
        }
    }
}
