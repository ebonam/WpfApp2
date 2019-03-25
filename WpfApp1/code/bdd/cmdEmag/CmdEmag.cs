using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp1.code.bdd.cmdEmag
{
    class CmdEmag
    {

        public string Ncommande;
        public string date;
        public string Hdeb;
        public string hfin;






     public   List<ArticleEmag> List = new List<ArticleEmag>();
        /// <summary>
        /// fonction qui permet de lire un presse papier pour en faire une liste d'article
        /// </summary>
        public void ReadCp(string text)
        {
            List = new List<ArticleEmag>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');
            /*
            Ncommande = xlRange.Cells[2, 6].Value2.ToString();
            date = xlRange.Cells[2, 11].Value2.ToString();
            Hdeb = xlRange.Cells[2, 12].Value2.ToString();
            hfin = xlRange.Cells[2, 13].Value2.ToString();
            */
            for (int i = 1; i < vs.Length; i++)
            {
                string line = vs[i];
                try
                {
                    if (!line.Equals(""))
                    {
                        Parameters p = Parameters.Instance();
                        string[] item = line.Split('\t');
                        ArticleEmag art = new ArticleEmag
                        {
                            _ean = item[p.emag.EAN - 1],//13],
                            _lib = item[p.emag.LIB - 1],// 15],
                            _qte = item[p.emag.QTE - 1],//16],
                            _prix = item[p.emag.PRIX - 1],//20],
                            _loc = item[p.emag.LOC - 1],//23]
                        };
                        Tri(art);
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }
        /// <summary>
        /// fonction qui permet de lire un fichier excel pour en faire une liste d'article
        /// </summary>
        /// <param name="filename">nom du fichier Excel</param>
        public void GetExcelFile(string filename)
        {
            List = new List<ArticleEmag>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@filename);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            Ncommande = xlRange.Cells[2, 6].Value2.ToString();
            date = xlRange.Cells[2, 11].Value2.ToString();
            Hdeb = xlRange.Cells[2, 12].Value2.ToString();
            hfin = xlRange.Cells[2, 13].Value2.ToString();
            for (int i = 2; i <= rowCount; i++)
            {
                ArticleEmag art = new ArticleEmag
                {
                    _ean = xlRange.Cells[i, 14].Value2.ToString(),
                    _lib = xlRange.Cells[i, 16].Value2.ToString(),
                    _qte = xlRange.Cells[i, 17].Value2.ToString(),
                    _prix = xlRange.Cells[i, 21].Value2.ToString(),

                    _loc = xlRange.Cells[i, 24].Value2.ToString()
                };


                Tri(art);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlApp);
        }


        public int FctQuifaittout(List<ArticleEmag> List, int i, _Worksheet xlWorksheet)
        {

            foreach (ArticleEmag product in List)
            {
                i++;
                xlWorksheet.Cells[i, 1].value2 = product._lib + "\n" + product._ean;
                xlWorksheet.Cells[i, 2].value2 = "=Transbar(" + product._ean + ")";
                MatchCollection gege = Regex.Matches(product._prix, "([0-9]*,[0-9]{0,2})");
                xlWorksheet.Cells[i, 3].value2 = gege[0].Value + "€";
                xlWorksheet.Cells[i, 4].value2 = product._qte;
                xlWorksheet.Cells[i, 5].value2 = product._loc;
            }
            i++;
            return i;
        }


        public void WriteExcelFile()
        {

            object misValue = System.Reflection.Missing.Value;
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\emag.xlsm"));

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


            xlWorksheet.PageSetup.PrintArea = "A$1:E" + i;
            xlWorkbook.PrintPreview();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false, misValue, misValue);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        List<ArticleEmag> Fleg = new List<ArticleEmag>();
        List<ArticleEmag> Surg = new List<ArticleEmag>();
        List<ArticleEmag> Liquide = new List<ArticleEmag>();
        List<ArticleEmag> Epicerie = new List<ArticleEmag>();
        List<ArticleEmag> DPH = new List<ArticleEmag>();
        List<ArticleEmag> FRAIS = new List<ArticleEmag>();
        List<ArticleEmag> NAL = new List<ArticleEmag>();
        List<ArticleEmag> NA = new List<ArticleEmag>();
        public static int Mtri(ArticleEmag x, ArticleEmag y)
        {
            int i = int.Parse(x._loc.Split('.')[0]);
            int j = int.Parse(y._loc.Split('.')[0]);
            return i.CompareTo(j);/*
            if (x._loc == null && y._loc == null) return 0;
            else if (x._loc == null) return -1;
            else if (y._loc == null) return 1;
            else return x._loc.CompareTo(y._loc);
            */
        }
        public void Tri(ArticleEmag ae)
        {

            //            NA.Sort(Mtri);
            try
            {
                int i = int.Parse(ae._loc.Split('.')[0]);

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
                else if (i < 28 && i % 2 == 0)
                {
                    Epicerie.Add(ae);
                    //epi
                }
                else if (i <= 42 && i % 2 == 0)
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


        //todo gerer liste plus le sort(dans params) 
        public void WriteExcelFileV2()
        {

            object misValue = System.Reflection.Missing.Value;
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\emag.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 2;
            string str = "";

            foreach (ArticleEmag product in List)
            {
                if (str == "" || str == product._sec)
                {
                    xlWorksheet.Cells[i, 1].value2 = product._sec;

                }
                i++;
                xlWorksheet.Cells[i, 1].value2 = product._lib + "\n" + product._ean;
                xlWorksheet.Cells[i, 2].value2 = "=Transbar(" + product._ean + ")";
                MatchCollection gege = Regex.Matches(product._prix, "([0-9]*,[0-9]{0,2})");
                xlWorksheet.Cells[i, 3].value2 = gege[0].Value + "€";
                xlWorksheet.Cells[i, 4].value2 = product._qte;
                xlWorksheet.Cells[i, 5].value2 = product._loc;
            }


            xlWorksheet.PageSetup.PrintArea = "A$1:E" + i;
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

