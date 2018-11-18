using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1.code.bdd.cmdVlep
{
    class VlepCmd
    {

        //Gâteaux pépites de chocolat Pépito Pépito 317241 24378 3048282900646  1.0 le paquet de 5 - 150 g 1,01€ 1,01€ 
        public static void Test(string sdsf)
        {
            List<ProductVlep> d = new List<ProductVlep>();
            var liness = sdsf.Split('\n');//Regex.Matches(sdsf, "\n");
            foreach (string l in liness)
            {
                MatchCollection gege= Regex.Matches(l, "\n");
                //
                var gencode = gege[2].Value;
                l.Replace(gege[0].Value, "");
                l.Replace(gege[1].Value, "");
                l.Replace(gege[2].Value, "");

                var gege2= Regex.Matches(l, "[0-9]+,[0-9]+€");
                var prix1= gege2[0].Value;
                string prix2 = gege2[1].Value;
                l.Replace(gege2[0].Value, "");
                l.Replace(gege2[1].Value, "");

                var gege3 = Regex.Matches(l, "[0-9]+\\.[0-9]+");
                var qte = gege3[0].Value;
                l.Replace(gege3[0].Value, "");
                string lib = l;//bordel retirer
                d.Add(new ProductVlep(gencode, prix1, prix2, qte, lib));
          
                foreach(var v in d) {
                    Console.WriteLine(d.ToString());
}
                //l.
                //d.Add(l.Value.Replace("^FS", "").Replace("^FD", ""));
            }
                return ;
        }
    }
}
