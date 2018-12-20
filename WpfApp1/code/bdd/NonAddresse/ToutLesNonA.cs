using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
namespace WpfApp1.code.bdd.NonAddresse
{
    public class ToutLesNonA
    {
        private List<NonAddresseS> _list;


        public void ReadCp(string text)
        {
            var bdd = Bdd.Instance();
            _list = new List<NonAddresseS>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');

            for (int i = 1; i < vs.Length; i++)
            {
                string line = vs[i];
                string[] item = line.Split('\t');
                NonAddresseS art = new NonAddresseS
                {
                    Lib = item[2],
                    Ean = long.Parse(item[3]),
                    Alle = int.Parse(item[8]),
                    Trave = int.Parse(item[9])


                };
                _list.Add(art);
            }
            foreach (NonAddresseS nonAddresseS in _list) {
                bdd.AddProduit(nonAddresseS);
            }


        }
        /**
         *
         */
        public void GetExcelFile(string fileName)
        {
            var bdd = Bdd.Instance();
            _list = new List<NonAddresseS>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@fileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
          
            for (int i = 2; i <= rowCount; i++)
            {
                NonAddresseS art = new NonAddresseS();
                for (int j = 1; j <= colCount; j++)
                {
                    switch (j)
                    {
                        case 3:
                            art.Ean = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 4:
                            art.Lib = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 9:
                            art.Alle = xlRange.Cells[i, j].Value2.ToString();
                            break;
                        case 10:
                            art.Trave = xlRange.Cells[i, j].Value2.ToString();
                            break;                    }
                }
                _list.Add(art);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            Marshal.ReleaseComObject(xlApp);
            foreach (NonAddresseS nonAddresseS in _list)
            {
                bdd.AddProduit(nonAddresseS);
            }

        }
    }
}
