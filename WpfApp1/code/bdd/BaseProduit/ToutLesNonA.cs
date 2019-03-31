using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WpfApp1.code.bdd.BaseProduit;
using Excel = Microsoft.Office.Interop.Excel;
namespace WpfApp1.code.bdd.NonAddresse
{
    public class ToutLesNonA
    {
        private List<NonAddresseS2> _list;


        public void ReadCp(string text)
        {
            var bdd = Bdd.Instance();
            _list = new List<NonAddresseS2>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');
            Parameters p = Parameters.Instance();
            for (int i = 1; i < vs.Length; i++)
            {
                try
                {
                    string line = vs[i];
                    string[] item = line.Split('\t');
                     NonAddresseS2 art = new NonAddresseS2();

                    art.Lib = item[p.prod.LIB-1];//3];
                    art.Ean = long.Parse(item[p.prod.EAN - 1]);//4]);
                    art.Alle = int.Parse(item[p.prod.Alle - 1]);//9]);
                    art.Trave = int.Parse(item[p.prod.Trave - 1]);//10]);
                    _list.Add(art);

                }
                catch (Exception e) { Console.WriteLine(e.Message); }
                
            }
            foreach (NonAddresseS2 nonAddresseS in _list)
            {
                bdd.AddProduit(nonAddresseS);

                Console.WriteLine("ok");
            }


        }
     /*   public void test()
        {
            NonAddresseS nonAddresseS;
            string srt = "";
            var m = Bdd.Instance().SearchLocProduit(long.Parse(srt));
            if (m.Count == 0) {
                Bdd.Instance().AddProduit();
            }else
            {
                if (nonAddresseS == tg) { noChange; } else
                {
                    change();

                }


            }

        }
        */


        /**
         *
         */
        public void GetExcelFile(string fileName)
        {
            var bdd = Bdd.Instance();
            _list = new List<NonAddresseS2>();
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
                NonAddresseS2 art = new NonAddresseS2();
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
                            break;
                    }
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
            foreach (NonAddresseS2 nonAddresseS in _list)
            {
                bdd.AddProduit(nonAddresseS);
            }

        }
    }
}
