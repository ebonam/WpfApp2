using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1.code
{
    class Fonctions
    {
        public static List<string> Test(string sdsf)
        {
            List<string> d = new List<string>();
            var liness = Regex.Matches(sdsf, "\\^FD(.+)(\\^FS)");
            foreach (Match l in liness)
                d.Add(l.Value.Replace("^FS", "").Replace("^FD", ""));
            return d;
        }
    }
}
