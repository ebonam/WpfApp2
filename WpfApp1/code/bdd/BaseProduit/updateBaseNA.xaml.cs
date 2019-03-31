using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.code.bdd.NonAddresse
{
    //undone
    /// <summary>
    /// Plus utile  sauf pour test 
    /// Logique d'interaction pour updateBaseNA.xaml
    /// </summary>
    public partial class UpdateBaseNA : UserControl
    {
       
        public UpdateBaseNA()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Fichier excel |*.xlsx",
                Title = "Selectionnez le fichier"
            };

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
