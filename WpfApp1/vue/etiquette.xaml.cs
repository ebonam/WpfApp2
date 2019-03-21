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
    /// goldé
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
        public string _nc { get; set; }

        public Etiquette()
        {

            InitializeComponent();
        }
        public void Setter(string str)
        {

            List<string> s = new List<string>();
            var liness = Regex.Matches(str, "\\^FD(.*)(\\^FS)");
            foreach (Match l in liness)
                s.Add(l.Value.Replace("^FS", "").Replace("^FD", ""));
            if (s.Count == 13)
            {

                _cde = s[0];
                _prep = s[1];
                _nom = s[2];
                _idclient = s[3];
                _info = s[5];
                _ncommandShort = s[6];
                _dat = s[7];
                _nc = "";
                _contennant = s[12];
            }
            else
            {
                /*
0   ^FO10,50^FDCde : 57150735^FS
1    ^FO540,50^FDPrep : 262609^FS
2    ^FO10,110^FDHarlet Justine^FS
3    ^FO650,110^FD4003086^FS
4    ^FO10,170^FD^FS
5    ^FO700,170^FDNC^FS
6   ^FO10,220^FDInfo : Zone SC3 Sec3 DPH, 7322PREP13^FS
7    ^FO10,230^FD0735^FS
8    ^FO10,480^FD22/12/2018 19:00^FS
9    ^FO10,530^FD*Retrait* ^FS
10    ^FO300,480^FD^FS
11    ^FO300,530^FDHYP FOURMIES^FS    
12    ^FO330,410^FDPICKING_DRIVE^FS
13    ^FO100,600^FDC002626090154565^FS
*/
                _cde = s[0];
                _prep = s[1];
                _nom = s[2];
                _idclient = s[3];
                _nc = "NC";
                _info = s[6];
                _ncommandShort = s[7];
                _dat = s[9];
                _contennant = s[13];


            }
            this.commande.Content = _cde;
            this.Prep.Content = _prep;
            this.nom.Content = _nom;
            this.idclient.Content = _idclient;
            this._lbNC.Content = this._nc;
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

