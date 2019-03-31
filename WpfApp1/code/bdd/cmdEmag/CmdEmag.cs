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




        public bool ReadCp(string text, int id)
        {
            bool retunr = true;
            List = new List<ArticleEmag>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');
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
                            Ncommande = "" + id,
                        };
                        try
                        {
                            string[] sr = art._loc.Split('.');
                            art._sec = SetSec(int.Parse(sr[0]));
                            art.trave = int.Parse(sr[0]);
                            art.rayon = int.Parse(sr[1]);

                        }
                        catch (Exception)
                        {


                            art._sec = "NA";

                        }
                        List.Add(art);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw new NotImplementedException();
                    retunr = false;
                }
            }
            return retunr;
        }

        //TODO CASSER LE CODE
        public List<ArticleEmag> List = new List<ArticleEmag>();
        /// <summary>
        /// fonction qui permet de lire un presse papier pour en faire une liste d'article
        /// </summary>
        public bool ReadCp(string text)
        {
            bool retunr = true;

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
                        try
                        {
                            string[] sr = art._loc.Split('.');
                            art._sec = SetSec(int.Parse(sr[0]));
                            art.trave = int.Parse(sr[0]);
                            art.rayon = int.Parse(sr[1]);

                        }
                        catch (Exception)
                        {


                            art._sec = "NA";

                        }
                        List.Add(art);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    retunr = false;
                }
            }
            return retunr;
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


                List.Add(art);
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

        public int SortRayon(ArticleEmag A, ArticleEmag B)
        {

            int cpr = A._sec.CompareTo(B._sec);
            if (cpr == 0)
            {
                cpr = A.rayon.CompareTo(B.rayon);
                if (cpr == 0)
                {

                    cpr = A.trave.CompareTo(B.trave);
                }
            }
            return cpr;
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
            int i = 1;
            List.Sort(SortRayon);//tofo rayon +alle 
            string str = "";
            foreach (ArticleEmag product in List)
            {
                bool t = false, t2 = false;
                bool test = true;
                while (test)
                {
                    try
                    {
                        if (str == "" || str != product._sec)
                        {
                            i++;
                            t = true;
                            xlWorksheet.Cells[i, 1].value2 = product._sec;
                            str = product._sec;
                        }
                        i++;
                        t2 = true;
                        Console.Write(product._lib + "\n" + product._ean);
                        xlWorksheet.Cells[i, 1].value2 = product._lib + "\n" + product._ean;
                        xlWorksheet.Cells[i, 2].value2 = "=Transbar(" + product._ean + ")";
                        MatchCollection gege = Regex.Matches(product._prix, "([0-9]*,[0-9]{0,2})");
                        xlWorksheet.Cells[i, 3].value2 = gege[0].Value + "€";
                        xlWorksheet.Cells[i, 4].value2 = product._qte;
                        xlWorksheet.Cells[i, 5].value2 = product._loc;
                        test = false;
                    }
                    catch (Exception)
                    {
                        if (t2) i--;
                        if (t) i--;
                    }
                }
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

