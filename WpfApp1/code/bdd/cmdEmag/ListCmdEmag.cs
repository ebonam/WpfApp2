using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
namespace WpfApp1.code.bdd.cmdEmag
{
    class ListCmdEmag
    {

        /// <summary>
        /// Liste de differentes commandes pour le tri 
        /// </summary>
        List<CmdEmag> cmdEmags = new List<CmdEmag>();


        /// <summary>
        /// Liste des articles trié sur un secteur 
        /// </summary>
        List<ArticleEmag> articleEmags = new List<ArticleEmag>();


        /// <summary>
        /// test l'ajout d'une commande, si erreur, retourne false sinon ajoute la commande dans cmdEmags
        /// </summary>
        /// <param name="text">text representant la commande </param>
        /// <param name="id"> int representant un numero de commande</param>
        /// <returns>retourne faux si erreur lors du traitement </returns>
        public bool Add(string text, int id)
        {
            bool r = true;

            try
            {
                var t = new CmdEmag();
                if (t.ReadCp(text, id))

                    cmdEmags.Add(t);
                else {
                    r = false;
                }
            }
            catch (Exception)
            {
                r = false;

            }

            return r;

        }

        public void Remove(int selectedIndex)
        {
            cmdEmags.RemoveAt(selectedIndex);
        }


        public int SortRayon(ArticleEmag A, ArticleEmag B)
        {


            int cpr = A.rayon.CompareTo(B.rayon);
            if (cpr == 0)
            {

                cpr = A.trave.CompareTo(B.trave);
            }

            return cpr;
        }
        public void WriteExcelFileV3(string sec)
        {

            foreach (CmdEmag cmdEmag in cmdEmags)
            {
                articleEmags.AddRange(cmdEmag.List.Where(x => x._sec == sec));
            }
            object misValue = System.Reflection.Missing.Value;
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Workbook xlWorkbook = xlApp.Workbooks.Open(System.IO.Path.Combine(exeDir, "excel\\emagmulti.xlsm"));
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            xlApp.Visible = true;
            xlApp.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityByUI;
            int i = 1;

            articleEmags.Sort(SortRayon);
            List<string> str = new List<string>();

            foreach (ArticleEmag product in articleEmags)
            {

                bool t = false;

                bool test = true;

                while (test)
                {
                    try
                    {

                        i++;
                        if (!str.Contains(product.Ncommande))
                        {
                            str.Add(product.Ncommande);
                           
                        }
                        t = true;
                        xlWorksheet.Cells[i, 1].value2 = product.Ncommande + "\n" + "N°" + (str.FindIndex(x => x.Equals(product.Ncommande)) + 1);
                        xlWorksheet.Cells[i, 2].value2 = product._lib + "\n" + product._ean;
                        xlWorksheet.Cells[i, 3].value2 = "=Transbar(" + product._ean + ")";
                        MatchCollection gege = Regex.Matches(product._prix, "([0-9]*,[0-9]{0,2})");
                        xlWorksheet.Cells[i, 4].value2 = gege[0].Value + "€";
                        xlWorksheet.Cells[i, 5].value2 = product._qte;
                        xlWorksheet.Cells[i, 6].value2 = product._loc;
                        test = false;
                    }
                    catch (Exception)
                    {
                        if (t) i--;

                    }
                }
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
            articleEmags.Clear();
        }


    }
}
