using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.code;
using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.vue
{
#pragma warning disable IDE1006 // Styles d'affectation de noms
    /// <summary>
    /// Logique d'interaction pour parametres.xaml
    /// </summary>
    public partial class parametres : UserControl
#pragma warning restore IDE1006 // Styles d'affectation de noms
    {

        public List<string> l;
        public parametres()
        {
            l = new List<string>();
            p = Parameters.Instance();
            InitializeComponent();
            PrintEmag();
            PrintProd();
            _comboSecteur0.ItemsSource = l;
            _comboSecteur1.ItemsSource = l;
            _listboxNomSecteur.ItemsSource = p.ps.nomSecteur;

            listBox1.ItemsSource = p.TGs.tgs;
            UpdateSelector();
        }
        /// <summary>
        /// Singleton pour les parametres
        /// </summary>
        public Parameters p;



        //TODO controle du flux utilisateur 
        /// <summary>
        /// ajoute une tg, et sauvegarde 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterTg(object sender, EventArgs e)
        {
            int rayon, trave;
            try
            {
                rayon = int.Parse(rayonTg.Text);
                trave = int.Parse(traveTg.Text);
                string str = (string)_comboSecteur1.SelectedItem;

                p.TGs.Ajout(rayon, trave, str);
                listBox1.ItemsSource = null;
                listBox1.ItemsSource = p.TGs.tgs;
                p.Sav();
            }
            catch (Exception)
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);

            }
        }
        /// <summary>
        /// retire une tg, et sauvegarde 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetirerTG(object sender, EventArgs e)
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
            p.Sav();
        }

        private void RetirerRayon(object sender, EventArgs e)
        {



            int selectedIndex = _listSecteurRayon.SelectedIndex;
            try
            {
                p.ps.DeRayon(selectedIndex, (string)_labelSecteur.Content);
            }
            catch
            {
            }
            _listSecteurRayon.ItemsSource = null;
            _listSecteurRayon.ItemsSource = p.ps.GetRayon((string)_labelSecteur.Content);
            p.Sav();
        }

        private void ViderMotCle(object sender, RoutedEventArgs e)
        {

        }
        private void ExporterMotCle(object sender, RoutedEventArgs e)
        {
            ListeMC m = new ListeMC();
            m.WriteExcelFile();

        }
        private void ImporterMotCle(object sender, RoutedEventArgs e)
        {

        }

        private void AfficherRayonSecteur(object sender, RoutedEventArgs e)
        {

            string str = (string)_comboSecteur0.SelectedItem;
            if (str != null && str!="")
            {
                _labelSecteur.Content = str;
                _listSecteurRayon.ItemsSource = null;
                _listSecteurRayon.ItemsSource = p.ps.GetRayon(str);
                _rayonSecteur.Visibility = Visibility.Visible;
            }
        }

        private void MAJBD(object sender, RoutedEventArgs e)
        {

        }
        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            var tlna = new ToutLesNonA();
            tlna.ReadCp(this.tb.Text);

        }


        private void AddSecteur(object sender, RoutedEventArgs e)
        {
            if (_nomSecteur.Text != null && _nomSecteur.Text != "")
            {
                p.ps.Add(_nomSecteur.Text);
                _listboxNomSecteur.ItemsSource = null;
                _listboxNomSecteur.ItemsSource = p.ps.nomSecteur;
                UpdateSelector();
                p.Sav();
                _nomSecteur.Text = "";
            }
        }
        private void UpdateSelector()
        {
            _comboSecteur0.ItemsSource = null;
            _comboSecteur1.ItemsSource = null;
            _comboSecteur0.ItemsSource = p.ps.nomSecteur;
            _comboSecteur1.ItemsSource = p.ps.nomSecteur;
            _rayonSecteur.Visibility = Visibility.Hidden;


        }

        private void ApplyClient(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void viderClient(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void updateClient(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

            private void AjouterRayonSecteur(object sender, EventArgs e)
        {
            string str = (string)_labelSecteur.Content;
            try
            {
                int i = int.Parse(RayonSecteurNum.Text);//test valeur ==nombre
                p.ps.AddRayon(str, RayonSecteurNum.Text);

                _listSecteurRayon.ItemsSource = null;
                _listSecteurRayon.ItemsSource = p.ps.GetRayon(str);
                p.Sav();
                RayonSecteurNum.Text = "";
            }
            catch (Exception )
            {

                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);
                //                throw new NotImplementedException();
            }


        }



        /// <summary>
        /// retire une tg, et sauvegarde 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetirerSec(object sender, EventArgs e)
        {
            int selectedIndex = _listboxNomSecteur.SelectedIndex;
            try
            {
                p.ps.Remov(selectedIndex);
            }
            catch
            {
            }
            _listboxNomSecteur.ItemsSource = null;
            _listboxNomSecteur.ItemsSource = p.ps.nomSecteur;
            UpdateSelector();
            p.Sav();
        }



        private void ApplyEmag(object sender, RoutedEventArgs e)
        {
            try
            {
                p.emag.EAN = int.Parse(this.EanEMag.Text);
                p.emag.LIB = int.Parse(this.LibEMag.Text);
                p.emag.LOC = int.Parse(this.LocEMag.Text);
                p.emag.PRIX = int.Parse(this.PrixEMag.Text);
                p.emag.QTE = int.Parse(this.QteEMag.Text);
                p.Sav();
            }
            catch (Exception)
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);


            }
        }
        private void PrintEmag()
        {

            this.EanEMag.Text = "" + p.emag.EAN;
            this.LibEMag.Text = "" + p.emag.LIB;
            LocEMag.Text = "" + p.emag.LOC;
            PrixEMag.Text = p.emag.PRIX + "";
            QteEMag.Text = "" + p.emag.QTE;
        }
        private void PrintProd()
        {

            this.EanProd.Text = "" + p.prod.EAN;
            this.LibProd.Text = "" + p.prod.LIB;
            AllePro.Text = "" + p.prod.Alle;
            TraveProd.Text = p.prod.Trave + "";
        }
        private void ApplyProd(object sender, RoutedEventArgs e)
        {
            try
            {
                p.prod.EAN = int.Parse(this.EanProd.Text);
                p.prod.LIB = int.Parse(this.LibProd.Text);
                p.prod.Alle = int.Parse(this.AllePro.Text);
                p.prod.Trave = int.Parse(this.TraveProd.Text);

                p.Sav();
            }
            catch (Exception)
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);


            }
        }
    }
}
