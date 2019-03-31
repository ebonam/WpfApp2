using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

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
            if (cmdEmag.ReadCp(this.tb.Text))
                cmdEmag.WriteExcelFileV2();
            else { MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK); }
        }
        
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            CmdEmagVue cmdEmag = new CmdEmagVue();
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Fichier excel |*.xlsx",
                Title = "Selectionnez le fichier"
            };

            if (openFileDialog1.ShowDialog() == true)
            {
                var tlna = new CmdEmag();
                //tlna.GetExcelFile(openFileDialog1.FileName);
                
                tlna.WriteExcelFileV2();
            }
        }

    }
}
