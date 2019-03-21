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
        private List<ProductVlep> d;

        private List<ProductVlep> Fleg = new List<ProductVlep>();
        private List<ProductVlep> Surg = new List<ProductVlep>();
        private List<ProductVlep> Liquide = new List<ProductVlep>();
        private List<ProductVlep> Epicerie = new List<ProductVlep>();
        private List<ProductVlep> DPH = new List<ProductVlep>();
        private List<ProductVlep> FRAIS = new List<ProductVlep>();
        private List<ProductVlep> NAL = new List<ProductVlep>();
        private List<ProductVlep> NA = new List<ProductVlep>();

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
            var liness = sdsf.Split('\n');
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
                    Tri(pv);          
                }
            }
        }
        /// <summary>
        /// ecrit dans un fichier pour impression
        /// </summary>
        public void WriteExcelFile()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\vlep.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 2;
            if (this.NA.Count != 0)
            {
                xlWorksheet.Cells[i, 1].value2 = "Non addressé";
                i = FctQuifaittout(NA, i, xlWorksheet);

            }
            if (this.Liquide.Count != 0)
            {
                Liquide.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "Liquide";
                i = FctQuifaittout(Liquide, i, xlWorksheet);
            }
            if (this.Epicerie.Count != 0)
            {
                Epicerie.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "Epicerie";
                i = FctQuifaittout(Epicerie, i, xlWorksheet);
            }
            if (this.DPH.Count != 0)
            {
                DPH.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "DPH";
                i = FctQuifaittout(DPH, i, xlWorksheet);
            }
            if (this.Fleg.Count != 0)
            {
                Fleg.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "Fruits et legumes";
                i = FctQuifaittout(Fleg, i, xlWorksheet);
            }
            if (this.FRAIS.Count != 0)
            {
                FRAIS.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "Frais";
                i = FctQuifaittout(FRAIS, i, xlWorksheet);
            }
            if (this.Surg.Count != 0)
            {
                Surg.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "Surgelé";
                i = FctQuifaittout(Surg, i, xlWorksheet);
            }
            if (this.NAL.Count != 0)
            {
                NAL.Sort(Mtri);
                xlWorksheet.Cells[i, 1].value2 = "NAL";
                i = FctQuifaittout(NAL, i, xlWorksheet);
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

        private int FctQuifaittout(List<ProductVlep> liste, int i, _Worksheet xlWorksheet)
        {
            foreach (ProductVlep product in liste)
            {
                i++;
                xlWorksheet.Cells[i, 1].value2 = product.Lib+"\n" + product.Gencode;
                xlWorksheet.Cells[i, 2].value2 = "=Transbar(" + product.Gencode + ")"; 
                xlWorksheet.Cells[i, 3].value2 = product.Prix1;
                xlWorksheet.Cells[i, 4].value2 = product.Qte;
                xlWorksheet.Cells[i, 5].value2 = product.Prix2;
                xlWorksheet.Cells[i, 6].value2 = product.Loc;
            }
            i++;
            return i;
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
            string str="";
            foreach (ProductVlep product in d)
            {
                i++;
                if (str == "" || str == product.Sec)
                {
                    xlWorksheet.Cells[i, 1].value2 = product.Sec;
                    i++;
                }
                
                xlWorksheet.Cells[i, 1].value2 = product.Lib + "\n" + product.Gencode;
                xlWorksheet.Cells[i, 2].value2 = "=Transbar(" + product.Gencode + ")";
                xlWorksheet.Cells[i, 3].value2 = product.Prix1;
                xlWorksheet.Cells[i, 4].value2 = product.Qte;
                xlWorksheet.Cells[i, 5].value2 = product.Prix2;
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

