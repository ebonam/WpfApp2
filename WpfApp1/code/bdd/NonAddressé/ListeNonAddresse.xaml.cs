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
using WpfApp1.code.bdd.NonAddressé;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour ListeNonAddresse.xaml
    /// </summary>
    public partial class ListeNonAddresse : UserControl
    {
        public ListeNonAddresse()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str =  ((ComboBoxItem)_combo.SelectedItem).Content.ToString();

            new ListeNA().ReadCp(this.tb.Text,str,true,true);
        }
    }
}
