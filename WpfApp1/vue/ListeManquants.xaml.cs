using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.code;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace WpfApp1.vue
{//TODO HERE
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
            if (textblock.Text != "")
            {
                var liness = this.textblock.Text.Split('\n');//Regex.Matches(sdsf, "\n");
                int i = 0;
                string l = liness[i];
                bool flag = true;
                bool flag2 = true;
                while (flag2 && flag && i < liness.Length)
                {
                    l = liness[i++];
                    if (l.Equals("") || l.Equals("\r") || l.Equals(" "))
                    {
                        flag = false;
                    }
                    else
                    {
                        Manquant m = new Manquant();
                        if (m.Fct(l))
                        { _manquants.Add(m); }
                        else
                        {
                            flag2 = false;
                        }
                    }

                }
                if (flag2)
                {
                    _manquants.Sort(Tri);
                    WriteExcelFile();
                }
                else
                {
                    MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);

                }
            }
            else
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);
            }
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
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\manquants.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;
            string st = "";
            foreach (Manquant manquant in _manquants)
            {
                bool flag = true;
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;
                while (flag)
                {
                    try
                    {
                        i++;
                        flag3 = true;
                        if (st == "" || flag1)
                        {
                            st = manquant._nomPrep;
                            xlWorksheet.Cells[i++, 1].value2 = manquant._nomPrep;
                            flag1 = true;

                        }
                        else if (st != manquant._nomPrep || flag2)
                        {
                            st = manquant._nomPrep;
                            xlWorksheet.Cells[i++, 1].value2 = manquant._nomPrep;
                            flag2 = true;
                        }
                        xlWorksheet.Cells[i, 1].value2 = manquant._date + manquant._heure;
                        xlWorksheet.Cells[i, 2].value2 = manquant._lib + "\n" + manquant._ean;
                        xlWorksheet.Cells[i, 3].value2 = "=Transbar(" + manquant._ean + ")";
                        xlWorksheet.Cells[i, 4].value2 = manquant._loca;
                        xlWorksheet.Cells[i, 5].value2 = manquant._Prixvente;
                        xlWorksheet.Cells[i, 6].value2 = manquant._qtecmd;
                        xlWorksheet.Cells[i, 7].value2 = manquant._qteFact;
                        xlWorksheet.Cells[i, 8].value2 = manquant._Prep;
                        xlWorksheet.Cells[i, 9].value2 = manquant._Ncmd;
                        xlWorksheet.Cells[i, 10].value2 = manquant._nomClient;
                        flag = false;
                    }
                    catch (Exception)
                    {
                        if (flag1) i--;
                        if (flag2) i--;
                        if (flag3) i--;
                    }
                }
            }
            this._manquants = new List<Manquant>();
            xlWorksheet.PageSetup.PrintArea = "A$1:I" + i;
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
            this.textblock.Text = "";
        }
    }
}

