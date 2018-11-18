using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Etiquette
    {
        public string _cde { get; set; }
        public string _Prep { get; set; }
        public string _nom { get; set; }
        public string _idclient { get; set; }
        public string _info { get; set; }
        public string _dat { get; set; }
        public string _ncommandShort { get; set; }
        public string _contennant { get; set; }
        public Etiquette(List<string> s) {
            _cde = s[0];
            _Prep = s[1];
            _nom = s[2];
            _idclient = s[3];
            _info = s[4];
            _ncommandShort = s[5];
            _dat = s[6];
            _contennant= s[9];
        }
    }
}
