using System;
using System.Text.RegularExpressions;

namespace WpfApp1.code
{
    public class Manquant
    {
        public Manquant() { }
        //todo changer pour le compareTO 
        public string _date;
        public string _heure;
        public string _sec;
        public string _loca;
        public long _ean;
        public string _lib;
        public string _qtecmd;
        public string _qteFact;
        public string _Ncmd;
        public string _Prep;
        public string _nomClient;
        public string _nomPrep;
        public string _Prixvente;

        public void fct(string line)
        {
            try
            {
                // string line = "84 22/12/2018 22/12/2018 22/12/2018 19:00 FRS 17.7.1.1 6123444 8714100385678 BAC CDO CREME VANILLE 500G 755 1.000 0.000 57150735 262605 Harlet Justine Antoine 3.070    0.000 0.000 0.000  PICKING_DRIVE";              
                string lib = line.Remove(0, 3);
                MatchCollection gege = Regex.Matches(lib, "([0-9]{2}\\/){2}([0-9]{4}) ");
                lib = lib.Replace(gege[0].Value, "");
                lib = lib.Replace(gege[1].Value, "");
                _date = gege[2].Value;
                lib = lib.Replace(gege[2].Value, "");
                gege = Regex.Matches(lib, "[0-9]{2}:[0-9]{2} ");
                _heure = gege[0].Value;
                lib = lib.Replace(gege[0].Value, "");
                gege = Regex.Matches(lib, "((\\w)*|([0-9]))");
                _sec = gege[0].Value;
                if (_sec == "99")
                {
                    lib = lib.Replace(gege[0].Value, "");
                    gege = Regex.Matches(lib, "(\\w)+ :([A-z]| )*");
                    _loca = gege[0].Value;
                    lib = lib.Replace(gege[0].Value, "");

                }
                else
                {
                    lib = lib.Replace(gege[0].Value, "");
                    gege = Regex.Matches(lib, "([0-9]+\\.){3}([0-9]+)");
                    if (gege.Count != 0)
                    {
                        _loca = gege[0].Value;
                        lib = lib.Replace(gege[0].Value, "");
                    }
                    else
                    {
                       
                        gege = Regex.Matches(lib, "(\\w)+ :([A-z]| )*");
                        _loca = gege[0].Value;
                        lib = lib.Replace(gege[0].Value, "");
                    }
                }
                gege = Regex.Matches(lib, "([0-9]+) ");
                _ean = long.Parse(gege[1].Value);
                lib = lib.Replace(gege[0].Value, "");
                lib = lib.Replace(gege[1].Value, "");
                gege = Regex.Matches(lib, "([0-9]+)\\.([0-9]+) ");
                var str = lib.Substring(0, lib.IndexOf(gege[0].Value) + 0);
                gege = Regex.Matches(lib, " ");
                lib = lib.Replace(str, "");
                _lib = str.Substring(0, (str.Length) - gege[gege.Count - 1].Value.Length - 3);
                gege = Regex.Matches(lib, "([0-9]+)\\.([0-9]+) ");
                _qtecmd = gege[0].Value;
                _qteFact = gege[1].Value;
                lib = lib.Replace(gege[0].Value, "");
                lib = lib.Replace(gege[1].Value, "");
                gege = Regex.Matches(lib, "([0-9]+) ");
                _Ncmd = gege[0].Value;
                _Prep = gege[1].Value;
                lib = lib.Replace(gege[0].Value, "");
                lib = lib.Replace(gege[1].Value, "");
                gege = Regex.Matches(lib, "([0-9]+)\\.([0-9]+) ");
                _Prixvente = gege[0].Value;
                str = lib.Substring(0, lib.IndexOf(gege[0].Value) + 0);
                gege = Regex.Matches(str, "(\\w+) ");

                lib = lib.Replace(str, "");
                _nomClient = str.Substring(0, (str.Length) - gege[gege.Count - 1].Value.Length - 1);
                _nomPrep = gege[gege.Count - 1].Value;
                Console.Write(_nomPrep);
            }
            catch (Exception e)
            {
                //215 : \w \w
                Console.Write(e.Message);

            }
        }
    }
}
