
using System;
using System.Runtime.InteropServices;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab
namespace WpfApp1.code
{   /**
    *classe de test
    **/
    class ReadXL
    {
        public static void GetExcelFile(object sender, RoutedEventArgs e)
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\antoine\Desktop\EXTR_ADR_LIN_49855396.xlsm");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;

            // xlWorkbook.PrintPreview();
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {

                    if (j == 1)
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                }
            }
            GC.Collect();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

    }
}
