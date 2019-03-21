using System.Windows;
using System.Windows.Controls;
using WpfApp1.code;
using WpfApp1.code.bdd.NonAddressé;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour ListeNonAddresse.xaml
    /// </summary>
    public partial class ListeNonAddresse : UserControl
    {
        Parameters p = Parameters.Instance();
        public ListeNonAddresse()
        {
            InitializeComponent();

            _combo.ItemsSource = null;
            _combo.ItemsSource = p.ps.nomSecteur;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = (string)_combo.SelectedItem;
            bool b, b1;
            b = (bool)checkAddresse.IsChecked;
            if (str == "" || str == null)
            {
                b1 = false;
            }
            else {
                b1 = (bool)checkMC.IsChecked;
            }
            
            new ListeNA().ReadCp(this.tb.Text, str, (bool)checkAddresse.IsChecked, b1);
        }
    }
}
