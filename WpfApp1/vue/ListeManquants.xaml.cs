using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using WpfApp1.code;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour ListeManquants.xaml
    /// </summary>
    public partial class ListeManquants : UserControl
    {
        public ListeManquants()
        {
            InitializeComponent();
        }

        public List<Manquant> _manquants = new List<Manquant>();

        public void ReadManquant()
        {
            var liness = this.textblock.Text.Split('\n');//Regex.Matches(sdsf, "\n");
            int i = 0;
            string l = liness[i];
            while (!l.Equals("\r") && !l.Equals(""))
            {
                Manquant m = new Manquant();
                m.fct(l);
                _manquants.Add(m);
                l = liness[++i];
                
            }
            _manquants.Sort(Tri);
            WriteExcelFile();
        }
        private int Tri(Manquant x, Manquant y)
        {
            int i = x._nomPrep.CompareTo(y._nomPrep);
            if (i == 0)
            {
                DateTime oDate = DateTime.ParseExact(x._date + x._heure, "dd/MM/yyyy HH:mm ", null);
                DateTime oDate2 = DateTime.ParseExact(y._date + y._heure, "dd/MM/yyyy HH:mm ", null);
                return oDate.CompareTo(oDate2);
            }
            else { return i; }
        }
        public void WriteExcelFile()
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\manquants.xlsx"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            string st = "";
            foreach (Manquant manquant in _manquants)
            {
                i++;
                if (st == "")
                {
                    st = manquant._nomPrep;
                    xlWorksheet.Cells[i, 1].value2 = manquant._nomPrep;
                    i++;
                }
                else if (st != manquant._nomPrep)
                {

                    xlWorksheet.Cells[i, 1].value2 = manquant._nomPrep;
                    i++;
                }
                try
                {
                    xlWorksheet.Cells[i, 1].value2 = manquant._date + manquant._heure;
                    xlWorksheet.Cells[i, 2].value2 = manquant._lib;
                    xlWorksheet.Cells[i, 3].value2 = manquant._ean;
                    xlWorksheet.Cells[i, 4].value2 = manquant._loca;
                    xlWorksheet.Cells[i, 5].value2 = manquant._Prixvente;
                    xlWorksheet.Cells[i, 6].value2 = manquant._qtecmd;
                    xlWorksheet.Cells[i, 7].value2 = manquant._qteFact;
                    xlWorksheet.Cells[i, 8].value2 = manquant._Prep;
                    xlWorksheet.Cells[i, 9].value2 = manquant._Ncmd;
                    xlWorksheet.Cells[i, 10].value2 = manquant._nomClient;


                }
                catch(Exception e) { i ++; }

            }
            this._manquants = new List<Manquant>();
                xlWorksheet.PageSetup.PrintArea = "A$1:K" + i;
            xlWorkbook.PrintPreview();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false, misValue, misValue);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ReadManquant();
        }
    }
}

