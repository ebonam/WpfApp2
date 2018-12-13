using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using WpfApp1.code;
using WpfApp1.code.bdd.cmdVlep;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            b.Click += ReadXL.getExcelFile;
        }

        private void Test2(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Test(object sender, RoutedEventArgs e)
        {
            List<ProductVlep> d = new List<ProductVlep>();
            var liness = this.sdsf.Text.Split('\n');//Regex.Matches(sdsf, "\n");
            foreach (string m in liness)
            {
                try
                {

                    var l = m;
                    MatchCollection gege = Regex.Matches(l, "[0-9]{4,}");
                    //
                    var gencode = gege[2].Value;
                    l = l.Replace(gege[0].Value, "");
                    l = l.Replace(gege[1].Value, "");
                    l = l.Replace(gege[2].Value, "");

                    var gege2 = Regex.Matches(l, "[0-9]+,[0-9]+€");
                    var prix1 = gege2[0].Value;
                    string prix2 = gege2[1].Value;
                    l = l.Replace(gege2[0].Value, "");
                    l = l.Replace(gege2[1].Value, "");

                    var gege3 = Regex.Matches(l, "[0-9]+\\.[0-9]+");
                    var qte = gege3[0].Value;
                    l = l.Replace(gege3[0].Value, "");
                    string lib = l;//bordel retirer
                    d.Add(new ProductVlep(long.Parse(gencode), prix1, prix2, qte, lib));


                    //l.
                    //d.Add(l.Value.Replace("^FS", "").Replace("^FD", ""));
                }


                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            foreach (var v in d)
            {
                Console.WriteLine(v.oString());
            }
            return;
        }

    }
}
//