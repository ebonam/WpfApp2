using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WpfApp1.code.bdd.cmdVlep
{
    public class VlepCmd
    {
        /// <summary>
        /// Liste de produit
        /// </summary>
        /// 

        private int Ncommande;
  
        public List<ProductVlep> d;
      
        /// <summary>
        /// Fonction de parse pour commande VLEP
        /// </summary>
        /// <param name="sdsf"></param>
        public bool Test(string sdsf, int cmd)
        {
            this.Ncommande = cmd;


return            Test(sdsf);

        }


        /// <summary>
        /// Fonction de parse pour commande VLEP
        /// </summary>
        /// <param name="sdsf"></param>
        public bool Test(string sdsf)
        {
            bool retunr=true;
            d = new List<ProductVlep>();
            var liness = sdsf.Split('\n');
            try
            {
                foreach (string l in liness)
                {
                    if (!l.Equals("\r") && !l.Equals(""))
                    {
                        string lib = l;
                        MatchCollection gege = Regex.Matches(lib, "([0-9]{4,13} ){2}");

                        MatchCollection gegebis = Regex.Matches(lib, "([0-9]{4,13} )");
                        var gencode = gegebis[2].Value;
                        lib = lib.Replace(gegebis[2].Value, "");
                        lib = lib.Replace(gegebis[1].Value, "");
                        lib = lib.Replace(gegebis[0].Value, "");
                        var gege2 = Regex.Matches(lib, "[0-9]+,[0-9]+€");
                        var prix1 = gege2[0].Value;
                        string prix2 = gege2[1].Value;
                        lib = lib.Replace(gege2[0].Value, "");
                        lib = lib.Replace(gege2[1].Value, "");
                        var gege3 = Regex.Matches(l, "[0-9]+\\.[0-9]+");
                        var qte = gege3[0].Value;
                        lib = lib.Replace(gege3[0].Value, "");
                        ProductVlep pv = new ProductVlep(long.Parse(gencode), prix1, prix2, qte, lib);

                        pv.Searchemplacement();
                        pv.Sec = SetSec(pv.rayon);
                        d.Add(pv);
                        //  Tri(pv);          
                    }
                }
            }
            catch (Exception) {
                retunr = false;
            }
            return retunr &&d.Count!=0;
        }

        public string SetSec(int rayon)
        {
            Parameters p = Parameters.Instance();
            foreach (Parameters.Defrayon sec in p.ps.secteurs)
            {
                if (sec.rayon.Contains("" + rayon))
                {
                    return sec.nom;
                }
            }
            return "NA";

        }

        public int SortRayon(ProductVlep A, ProductVlep B)
        {

            int cpr = A.Sec.CompareTo(B.Sec);
            if (cpr == 0)
            {

                cpr = A.rayon.CompareTo(B.rayon);
                if (cpr == 0)
                {

                    cpr = A.Alle.CompareTo(B.Alle);
                }
            }
            return cpr;
        }

       

        //@todo
        public void WriteExcelFileV2()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\vlep.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            d.Sort(SortRayon);//tofo rayon +alle 

            string str = "";
            foreach (ProductVlep product in d)
            {
                bool t = false, t2 = false;

                bool test = true;

                while (test)
                {
                    try
                    {
                        i++;
                        t = true;
                        if (str == "" || str != product.Sec)
                        {
                            str = product.Sec;
                            xlWorksheet.Cells[i, 1].value2 = product.Sec;
                            i++;
                            t2 = true;
                        }

                        xlWorksheet.Cells[i, 1].value2 = product.Lib + "\n" + product.Gencode;
                        xlWorksheet.Cells[i, 2].value2 = "=Transbar(" + product.Gencode + ")";
                        xlWorksheet.Cells[i, 3].value2 = product.Prix1;
                        xlWorksheet.Cells[i, 4].value2 = product.Qte;
                        xlWorksheet.Cells[i, 5].value2 = product.Prix2;
                        xlWorksheet.Cells[i, 6].value2 = product.Loc;
                        test = false;
                    }
                    catch (Exception)
                    {
                        if (t2) i--;
                        if (t) i--;

                    }
                }

            }
            xlWorksheet.PageSetup.PrintArea = "A$1:F" + i;
            xlWorkbook.PrintPreview();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false, misValue, misValue);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
/*
public void Tri(ProductVlep ae)
{

    //            NA.Sort(Mtri);
    try
    {
        int i = int.Parse(ae.Loc.Split('.')[0]);

        if (i == 25)
        {
            Fleg.Add(ae);
            //fleg
        }
        if (i == 13 || i == 15)
        {
            Surg.Add(ae);
            //surg

        }
        if (i < 7 || i == 8 || i == 10)
        {
            Liquide.Add(ae);
            //liquide
        }
        else if (i > 101)
        {
            FRAIS.Add(ae);
            //    Frais / boucherie

        }
        else if (i<= 14 && i < 28 && i % 2 == 0)
        {
            Epicerie.Add(ae);
            //epi
        }
        else if (i>=28 && i <= 42 && i % 2 == 0)
        {
            //DPH
            DPH.Add(ae);
        }
        else if (i % 2 == 1 && i <= 23)
        {
            FRAIS.Add(ae);
        }
        else
        {
            NAL.Add(ae);//NAL}
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        NA.Add(ae);
    }
}

}
}
*/
