using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.code;
using WpfApp1.code.bdd;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour AjouterNA.xaml
    /// </summary>
    public partial class AjouterNA : UserControl
    {
        Parameters p = Parameters.Instance();
        public AjouterNA()
        {
            InitializeComponent();
            _Valider.Click += Ajouter;
            _combo.ItemsSource = null;
            _combo.ItemsSource = p.ps.nomSecteur;
        }
        private void Listee(object sender, RoutedEventArgs e)
        {
            Bdd.Instance().ListeNA();
        }
        private void Ajouter(object sender, RoutedEventArgs e)
        {
            try
            {
                int j = int.Parse(this._rayon.Text);
                string s, s2;
                s = this._MC.Text.ToUpper();
                s2 = (string)_combo.SelectedItem;
                if (s != " " && s != null && s2 != "" && s2 != null)
                {
                    Bdd.Instance().AddNA(s, j, (bool)this._bool.IsChecked, s2);
                    this._rayon.Text = "";
                    this._MC.Text = "";
                }
                else
                {
                    MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Aide_Rayon aide_Rayon = new Aide_Rayon();
            aide_Rayon.ShowDialog();
        }
    }
}
