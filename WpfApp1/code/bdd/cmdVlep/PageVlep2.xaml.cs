using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.code.bdd.cmdVlep
{
    /// <summary>
    /// Logique d'interaction pour PageVlep2.xaml
    /// </summary>
    public partial class PageVlep2 : UserControl
    {
        List<VlepCmd> vlepCmds = new List<VlepCmd>();
        Parameters p;
        List<string> l;
        List<string> ListeCMD;

        //
        ListeCmdVlep listCmd = new ListeCmdVlep();
        //

        public PageVlep2()
        {
            l = new List<string>();
            ListeCMD = new List<string>();
            p = Parameters.Instance();
            InitializeComponent();

            _comboSecteur0.ItemsSource = p.ps.nomSecteur;
            _listboxNomSecteur.ItemsSource = l;





        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                listCmd.add(this.tb.Text, int.Parse(this.nomCommande.Text));
                l.Add(this.nomCommande.Text);
                _listboxNomSecteur.ItemsSource = null;
                _listboxNomSecteur.ItemsSource = l;
                this.tb.Text = "";
                this.nomCommande.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);

                //    throw new NotImplementedException();
            }

            /*
            throw new NotImplementedException();
            VlepCmd vlepCmd = new VlepCmd();
            vlepCmd.Test(textblock.Text);
            vlepCmd.WriteExcelFileV2();*/
        }

        private void AfficherRayonSecteur(object sender, RoutedEventArgs e)
        {
            string str = (string)_comboSecteur0.SelectedItem;
            if (str != null && str != "" && l.Count != 0)
            {

                this.listCmd.WriteExcelFileV2(str);
            }

        }

        private void RetirerSec(object sender, EventArgs e)
        {
            int selectedIndex = _listboxNomSecteur.SelectedIndex;
            try
            {
                this.l.RemoveAt(selectedIndex);   //   p.ps.Remov(selectedIndex);
                this.listCmd.Remove(selectedIndex);
            }
            catch
            {
            }
            _listboxNomSecteur.ItemsSource = null;
            _listboxNomSecteur.ItemsSource = l;
        }

        private void UpdateSelector()
        {
            _comboSecteur0.ItemsSource = null;
            _comboSecteur0.ItemsSource = p.ps.nomSecteur;
        }
    }
}

