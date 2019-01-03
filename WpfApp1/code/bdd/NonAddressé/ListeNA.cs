using System;
using System.Collections.Generic;

namespace WpfApp1.code.bdd.NonAddressé
{
    class ListeNA
    {
        public List<NA> _NAs;
        public void ReadCp(string text)
        {
            _NAs = new List<NA>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');
            for (int i = 1; i < vs.Length; i++)
            {
                try
                {
                    string line = vs[i];
                    string[] item = line.Split('\t');
                    var na = new NA();
                    na.Lib = item[0];
                    na.Ean = long.Parse(item[1]);
                    _NAs.Add(na);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            foreach (NA nonAddresseS in _NAs)
            {
                Console.WriteLine("ok");
            }
        }
    }
}
