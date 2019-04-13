
using System;
using System.Windows;
using WpfApp1.code;
using WpfApp1.code.bdd;
using WpfApp1.code.bdd.BaseProduit;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour progessBar.xaml
    /// </summary>
    public partial class progessBar : Window
    {
        public int _nbaTraiter = 0;
        public int _nbTraite = 0;
        public int _nbChange = 0;
        public int _nbignoré = 0;
        public int _nberreur = 0;
        public int _identique = 0;
        private int _nbAjoute = 0;

        public progessBar()
        {
            InitializeComponent();
            this.progressb.Maximum = 10;
            progressb.Minimum = 0;
            progressb.SmallChange = 1;
            //progressb

        }


        public void blal(string text)
        {
            //var tlna = new ToutLesNonA();
            //tlna.ReadCp("");



            var bdd = Bdd.Instance();
            //     _list = new List<NonAddresseS2>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');
            this._nbaTraiter = vs.Length;
            this.progressb.Maximum = vs.Length;
            Parameters p = Parameters.Instance();
            for (int i = 1; i < vs.Length; i++)
            {
                try
                {
                    string line = vs[i];
                    string[] item = line.Split('\t');
                    NonAddresseS2 art = new NonAddresseS2
                    {
                        Lib = item[p.prod.LIB - 1],//3];
                        Ean = long.Parse(item[p.prod.EAN - 1]),//4]);
                        Alle = int.Parse(item[p.prod.Alle - 1]),//9]);
                        Trave = int.Parse(item[p.prod.Trave - 1])//10]);
                    };
                    //         _list.Add(art);
                    Maj(bdd.AddProduit(art));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }
        public void Maj(int j)
        {

            switch (j)
            {
                case 0:
                    _nbaTraiter--;
                    _nbignoré++;
                    _nbTraite++;

                    break;
                case 1:
                    _nbaTraiter--;
                    _nbTraite++;
                    _nbAjoute++;
                    break;
                case 2:
                    _nbaTraiter--;
                    _nbTraite++;

                    _nbChange++;
                    break;
            }
            progressb.Value = _nbTraite;
        }

    }
}
