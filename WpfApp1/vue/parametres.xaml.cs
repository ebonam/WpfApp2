using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.code;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour parametres.xaml
    /// </summary>
    public partial class parametres : UserControl
    {
        public parametres()
        {
            InitializeComponent();

            //p.charge();
            listBox1.ItemsSource = p.TGs.tgs;
        }
        /// <summary>
        /// Singleton pour les parametres
        /// </summary>
       public Parameters p = Parameters.Instance();



        //TODO controle du flux utilisateur 
        /// <summary>
        /// ajoute une tg, et sauvegarde 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterTg(object sender, EventArgs e)
        {
            string str = ((ComboBoxItem)_comboSecteur1.SelectedItem).Content.ToString();
            p.TGs.Ajout(rayonTg.Text,traveTg.Text,str);       
            listBox1.ItemsSource = null;
            listBox1.ItemsSource = p.TGs.tgs;
            p.sav();
        }
        /// <summary>
        /// retire une tg, et sauvegarde 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retirerTG(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            try
            {
                p.TGs.Remov(selectedIndex);
            }
            catch
            {
            }
            listBox1.ItemsSource = null;
            listBox1.ItemsSource = p.TGs.tgs;
            p.sav();
        }

        private void ViderMotCle(object sender, RoutedEventArgs e)
        {

        }
        private void ExporterMotCle(object sender, RoutedEventArgs e)
        {

        }
        private void ImporterMotCle(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        } private void MAJBD(object sender, RoutedEventArgs e)
        {

        }
    }
}
