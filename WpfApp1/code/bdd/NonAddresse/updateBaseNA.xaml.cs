using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.code.bdd.NonAddresse
{
    /// <summary>
    /// Logique d'interaction pour updateBaseNA.xaml
    /// </summary>
    public partial class UpdateBaseNA : UserControl
    {
       
        public UpdateBaseNA()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Fichier excel |*.xlsx";
            openFileDialog1.Title = "Selectionnez le fichier";

            if (openFileDialog1.ShowDialog() ==true)
            {
                var tlna = new ToutLesNonA();
                tlna.GetExcelFile(openFileDialog1.FileName);
            }
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            var tlna=new ToutLesNonA();
            tlna.ReadCp(this.tb.Text);

        }
    }
}
