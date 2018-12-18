using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.code.bdd.cmdEmag
{
    /// <summary>
    /// Logique d'interaction pour cmdEmag.xaml
    /// </summary>
    public partial class CmdEmagVue : UserControl
    {
        public CmdEmagVue()
        {
            InitializeComponent();
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            CmdEmag cmdEmag = new CmdEmag();
            cmdEmag.ReadCp(this.tb.Text);
            cmdEmag.WriteExcelFile();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            CmdEmagVue cmdEmag = new CmdEmagVue();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Fichier excel |*.xlsx";
            openFileDialog1.Title = "Selectionnez le fichier";

            if (openFileDialog1.ShowDialog() == true)
            {
                var tlna = new CmdEmag();
                tlna.GetExcelFile(openFileDialog1.FileName);
            }
        }
    }
}
