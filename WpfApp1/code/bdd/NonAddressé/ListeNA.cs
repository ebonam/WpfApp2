using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace WpfApp1.code.bdd.NonAddressé
{
    class ListeNA
    {
        public List<NA2> _NAs;
        public ListMotClé _listMotClé;
        public ListeGencode _listGencode;
        public void ReadCp(string text, string str2, bool gencode, bool motcle)
        {
            _NAs = new List<NA2>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');
            for (int i = 1; i < vs.Length - 1; i++)
            {
                try
                {
                    string line = vs[i];
                    string[] item = line.Split('\t');
                    if (item.Length == 2)
                    {
                        if (item[1]!="EAN" && item[1] != "")
                        {
                            var na = new NA2();
                            na.Lib = item[0];
                            na.Ean = item[1];// long.Parse(item[1]);
                            _NAs.Add(na);
                        }
                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            if (gencode)
            {
                _listGencode = new ListeGencode();
                _NAs = _listGencode.TriDesFamilles(_NAs);
            }
            if (motcle)
            {
                _listMotClé = new ListMotClé(str2);
                _NAs = _listMotClé.TriDesFamilles(_NAs);
            }
            WriteExcelFile();
            //  WriteExcelFil2e();



        }
        public void WriteExcelFil2e()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\test2.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;

            foreach (NA2 nonAddresseS in _listMotClé._NaMC)
            {
                i++;
                xlWorksheet.Cells[i, 1].value2 = nonAddresseS.Lib;
                xlWorksheet.Cells[i, 2].value2 = nonAddresseS.Ean;
                xlWorksheet.Cells[i, 3].value2 = nonAddresseS.loc;
            }
            i++;


            xlWorksheet.PageSetup.PrintArea = "A$1:F" + i;
            //    xlWorkbook.PrintPreview();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false, misValue, misValue);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);


        }

        public void WriteExcelFile()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\na.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 4;
            string label = "";
            foreach (NA2 nonAddresseS in _listGencode._NaMC)//METTRE GENCODE
            {

                if (label == "" || label != nonAddresseS.loc) {

                    label = nonAddresseS.loc;
                    xlWorksheet.Cells[i, 2].value2 = "Localisation=" + label;
                    i++;
                }
                xlWorksheet.Cells[i, 2].value2 = nonAddresseS.Lib;
                xlWorksheet.Cells[i, 4].value2 = nonAddresseS.Ean;
                i++;
            }

            foreach (NA2 nonAddresseS in _listMotClé._NaMC)//METTRE GENCODE
            {
                if (label == "" || label != nonAddresseS.loc)
                {
                    label = nonAddresseS.loc;
                    xlWorksheet.Cells[i, 2].value2 = label + " : " + nonAddresseS.rayon;
                    i++;
                }
                xlWorksheet.Cells[i, 2].value2 = nonAddresseS.Lib;
                xlWorksheet.Cells[i, 4].value2 = nonAddresseS.Ean;
                i++;
            }

            i++;

            foreach (NA2 nonAddresseS in _NAs)//METTRE GENCODE
            {
                xlWorksheet.Cells[i, 2].value2 = nonAddresseS.Lib;
                xlWorksheet.Cells[i, 4].value2 = nonAddresseS.Ean;
                i++;
            }







            xlWorksheet.PageSetup.PrintArea = "B$3:D" + i;
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
