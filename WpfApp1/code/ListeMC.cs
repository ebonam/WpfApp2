using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WpfApp1.code.bdd;
namespace WpfApp1.code
{
    class ListeMC
    {
       public List<NA> nAbdds;

        public ListeMC()
        {
            this.nAbdds = new List<NA>();
        }

        public void WriteExcelFile()
        {
            this.nAbdds = Bdd.Instance().ListeNA();

            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\motcle.xlsx"));
            //TODO
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 2;
            foreach (NA f in nAbdds)
            {

                xlWorksheet.Cells[i, 1].value2 = f._Id;
                xlWorksheet.Cells[i, 2].value2 = f._Nom;
                xlWorksheet.Cells[i, 3].value2 = f._Rayon;
                xlWorksheet.Cells[i, 4].value2 = f._sec;
                xlWorksheet.Cells[i, 5].value2 = f._motcomplet;
                i++;
            }

          
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false, misValue, misValue);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
        public void ReadExcelFile(string s)
        {
            string[] s1 = s.Split('\n');
            Bdd.Instance().VideNA();
            for (int i = 0; i > s1.Length; i++)
            {
                NA f = new NA();
                string[] s2 = s1[i].Split('\t');
                f._Id = int.Parse(s2[0]);
                f._Nom = s2[1];
                f._Rayon = int.Parse(s2[2]);
                f._sec = s2[3];
                f._motcomplet = bool.Parse(s2[4]);
                
                Bdd.Instance().AddNA(f);
            }
        }


    }
}
