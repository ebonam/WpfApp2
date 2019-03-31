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

namespace WpfApp1.code.bdd.cmdVlep
{
    /// <summary>
    /// Logique d'interaction pour PageVlep.xaml
    /// </summary>
    public partial class PageVlep : UserControl
    {
        public PageVlep()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VlepCmd vlepCmd= new VlepCmd();
            if(vlepCmd.Test(textblock.Text))
            vlepCmd.WriteExcelFileV2();
            else MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);
        }
    }
}
