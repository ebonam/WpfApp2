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
        //@TODO 
        //@todo 
        //@Todo 
        public void readCp()
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\EXTR_DETAILCDE_52188893.xlsx");
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
                            art.ean = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 16:
                            art.lib = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 17:
                            art.qte = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 21:
                            art.prix = xlRange.Cells[i, j].Value2.ToString();
                            break;

                        case 24:
                            art.loc = xlRange.Cells[i, j].Value2.ToString();
                            break;


                    }
                    //write the value to the console

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


        public void getExcelFile()
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"D:\EXTR_DETAILCDE_52188893.xlsx");
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
                            art.ean = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 16:
                            art.lib = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 17:
                            art.qte = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 21:
                            art.prix = xlRange.Cells[i, j].Value2.ToString();
                            break;

                        case 24:
                            art.loc = xlRange.Cells[i, j].Value2.ToString();
                            break;


                    }
                    //write the value to the console

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

        public void writeExcelFile()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\antoine\Desktop\test.xlsm");
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            foreach (ArticleEmag product in List)
            {
                xlWorksheet.Cells[i, 1].value2 = product.lib;
                xlWorksheet.Cells[i, 2].value2 = product.ean;
                xlWorksheet.Cells[i, 3].value2 = product.prix;
                xlWorksheet.Cells[i, 4].value2 = product.qte;
                xlWorksheet.Cells[i, 5].value2 = product.loc;
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
