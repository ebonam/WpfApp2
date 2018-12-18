using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp1.code.bdd.cmdEmag
{
    class CmdEmag
    {

        public string Ncommande;
        public string date;
        public string Hdeb;
        public string hfin;
        List<ArticleEmag> List = new List<ArticleEmag>();
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
                string[] item = line.Split('\t');
                ArticleEmag art = new ArticleEmag
                {
                    _ean = item[13],
                    _lib = item[15],
                    _qte = item[16],
                    _prix = item[20],
                    _loc = item[23]
                };
                List.Add(art);
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
                ArticleEmag art = new ArticleEmag();
                for (int j = 1; j <= colCount; j++)
                {
                    switch (j)
                    {
                        case 14:
                            art._ean = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 16:
                            art._lib = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 17:
                            art._qte = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 21:
                            art._prix = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 24:
                            art._loc = xlRange.Cells[i, j].Value2.ToString();
                            break;
                    }
                }
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

        public void WriteExcelFile()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\antoine\Desktop\test.xlsm");
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            foreach (ArticleEmag product in List)
            {
                xlWorksheet.Cells[i, 1].value2 = product._lib;
                xlWorksheet.Cells[i, 2].value2 = product._ean;
                xlWorksheet.Cells[i, 3].value2 = product._prix;
                xlWorksheet.Cells[i, 4].value2 = product._qte;
                xlWorksheet.Cells[i, 5].value2 = product._loc;
                i++;

            }
            xlWorkbook.PrintPreview();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
