using BarcodeLib;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Etiquette : UserControl
    {

        public string _cde { get; set; }
        public string _prep { get; set; }
        public string _nom { get; set; }
        public string _idclient { get; set; }
        public string _info { get; set; }
        public string _dat { get; set; }
        public string _ncommandShort { get; set; }
        public string _contennant { get; set; }

        public Etiquette()
        {

            InitializeComponent();
        }
        public void Setter(string str)
        {

            List<string> s = new List<string>();
            var liness = Regex.Matches(str, "\\^FD(.+)(\\^FS)");
            foreach (Match l in liness)
                s.Add(l.Value.Replace("^FS", "").Replace("^FD", ""));


            _cde = s[0];
            _prep = s[1];
            _nom = s[2];
            _idclient = s[3];
            _info = s[4];
            _ncommandShort = s[5];
            _dat = s[6];
            _contennant = s[10];
            this.commande.Content = _cde;
            this.Prep.Content = _prep;
            this.nom.Content = _nom;
            this.idclient.Content = _idclient;
            zone.Content = _info;
            cmdShort.Content = _ncommandShort;
            date.Content = _dat;
            cont.Content=_contennant;
            var dt = new DateTime();
            dt = DateTime.Now;
            string alea = "" + dt.Ticks + ".png";
            var b = new Barcode(); 
            var img = b.Encode(TYPE.CODE128B, _contennant,250,50);
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            img.Save(System.IO.Path.Combine(exeDir, "excel\\" + alea));
            this.test.Source = new BitmapImage(new Uri(System.IO.Path.Combine(exeDir, "excel\\" + alea)));
   
        }




    }
}

