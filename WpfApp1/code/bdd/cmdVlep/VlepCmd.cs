using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;      

namespace WpfApp1.code.bdd.cmdVlep
{
    public class VlepCmd
    {
        /// <summary>
        /// Liste de produit
        /// </summary>
        private List<ProductVlep> d;

        List<ProductVlep> Fleg = new List<ProductVlep>();
        List<ProductVlep> Surg = new List<ProductVlep>();
        List<ProductVlep> Liquide = new List<ProductVlep>();
        List<ProductVlep> Epicerie = new List<ProductVlep>();
        List<ProductVlep> DPH = new List<ProductVlep>();
        List<ProductVlep> FRAIS = new List<ProductVlep>();
        List<ProductVlep> NAL = new List<ProductVlep>();
        List<ProductVlep> NA = new List<ProductVlep>();
        public static int Mtri(ProductVlep x, ProductVlep y)
        {
            int i = int.Parse(x.Loc.Split('.')[0]);
            int j = int.Parse(y.Loc.Split('.')[0]);
            return i.CompareTo(j);/*
            if (x._loc == null && y._loc == null) return 0;
            else if (x._loc == null) return -1;
            else if (y._loc == null) return 1;
            else return x._loc.CompareTo(y._loc);
            */
        }


        /// <summary>
        /// Fonction de parse pour commande VLEP
        /// </summary>
        /// <param name="sdsf"></param>
        public void Test(string sdsf)
        {
            d = new List<ProductVlep>();
            var liness = sdsf.Split('\n');//Regex.Matches(sdsf, "\n");
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
                    ProductVlep pv = new ProductVlep(long.Parse(gencode),prix1, prix2, qte, lib);
                    pv.Searchemplacement();
                    d.Add(pv);
                }
            }
        }
        //Todo

            /// <summary>
            /// ecrit dans un fichier pour impression
            /// </summary>
        public void WriteExcelFile()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;

            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\vlep.xlsx"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI; 
            int i = 1;
            foreach(ProductVlep product in d)
            {
                i++;
                xlWorksheet.Cells[i, 1].value2 =product.Lib;
                    xlWorksheet.Cells[i, 2].value2 =product.Gencode;
                    xlWorksheet.Cells[i, 3].value2 =product.Prix1;
                    xlWorksheet.Cells[i, 4].value2 =product.Qte;
                    xlWorksheet.Cells[i, 5].value2 =product.Prix2;
                    xlWorksheet.Cells[i, 6].value2 = product.Loc;
                
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

