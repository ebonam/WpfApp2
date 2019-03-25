using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
namespace WpfApp1.code.bdd.cmdEmag
{
    class ListCmdEmag
    {
        List<CmdEmag> cmdEmags=new List<CmdEmag>();
        List<ArticleEmag> articleEmags=new List<ArticleEmag>();

        public void add(string text) {
            var t = new CmdEmag();
            t.ReadCp(text);
            cmdEmags.Add(t);
        }





        public void WriteExcelFileV3(string sec)
        {
            articleEmags.AddRange(cmdEmags[0].List.Where(x=>x._sec==sec));
            
            object misValue = System.Reflection.Missing.Value;
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\emag.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            List<string> str = new List<string>() ;

            foreach (ArticleEmag product in articleEmags)
            {

                i++;
                if (!str.Contains(product.Ncommande))
                {
                    str.Add(product.Ncommande);

                }
                xlWorksheet.Cells[i, 1].value2 = product.Ncommande+"\n" + str.FindIndex(x => x.Equals(product.Ncommande));
                xlWorksheet.Cells[i, 2].value2 = product._lib + "\n" + product._ean;
                xlWorksheet.Cells[i, 3].value2 = "=Transbar(" + product._ean + ")";
                MatchCollection gege = Regex.Matches(product._prix, "([0-9]*,[0-9]{0,2})");
                xlWorksheet.Cells[i, 4].value2 = gege[0].Value + "€";
                xlWorksheet.Cells[i, 5].value2 = product._qte;
                xlWorksheet.Cells[i, 6].value2 = product._loc;
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
