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
            ListeGencode listeGencode = new ListeGencode();
            ListMotClé listeMC = new ListMotClé();
            _NAs = listeGencode.TriDesFamilles(_NAs);
            _NAs = listeMC.TriDesFamilles(_NAs);

            _NAs.Sort(Mtri);
        }
        public static int Mtri(NA x, NA y)
        {
            return x.Lib.CompareTo(y.Lib);
        }
    }
}
