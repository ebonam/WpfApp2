using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


namespace WpfApp1.code.bdd.cmdVlep
{
    class ListeCmdVlep
    {

        public void add(string cmd, string sdsf)
        {
            var b = new VlepCmd();
            b.Test(sdsf, cmd);
            cmdVleps.Add(b);
        }




        List<VlepCmd> cmdVleps = new List<VlepCmd>();
        List<ProductVlep> articleVleps = new List<ProductVlep>();
        public void WriteExcelFileV2(string sec)
        {
            articleVleps.AddRange(cmdVleps[0].d.Where(x => x.Sec == sec));
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\vlep.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            List<string> str = new List<string>();

            foreach (ProductVlep product in articleVleps)
            {
                i++;
                if (!str.Contains(product.nCommande))
                {
                    str.Add(product.nCommande);

                }

                xlWorksheet.Cells[i, 1].value2 = product.nCommande + str.FindIndex(x => x.Equals(product.nCommande)); ;
                xlWorksheet.Cells[i, 2].value2 = product.Lib + "\n" + product.Gencode;
                xlWorksheet.Cells[i, 3].value2 = "=Transbar(" + product.Gencode + ")";
                xlWorksheet.Cells[i, 4].value2 = product.Prix1;
                xlWorksheet.Cells[i, 5].value2 = product.Qte;
                xlWorksheet.Cells[i, 6].value2 = product.Prix2;
                xlWorksheet.Cells[i, 7].value2 = product.Loc;
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
