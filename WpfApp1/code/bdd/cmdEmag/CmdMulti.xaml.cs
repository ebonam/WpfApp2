using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.code.bdd.cmdEmag
{
    /// <summary>
    /// Logique d'interaction pour CmdMulti.xaml
    /// </summary>
    public partial class CmdMulti : UserControl
    {
        List<CmdEmag> vlepCmds = new List<CmdEmag>();
        Parameters p;
        List<string> l;
        List<string> ListeCMD;
        public CmdMulti()
        {
            l = new List<string>();
            ListeCMD = new List<string>();
            p = Parameters.Instance();
            InitializeComponent();
            _comboSecteur0.ItemsSource = p.ps.nomSecteur;
            _listboxNomSecteur.ItemsSource = l;
        }
        

        ListCmdEmag listCmd = new ListCmdEmag();
        private void Validate_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                listCmd.Add(this.tb.Text, int.Parse(this.NumCmd.Text));
                l.Add(this.NumCmd.Text);
                _listboxNomSecteur.ItemsSource = null;
                _listboxNomSecteur.ItemsSource = l;
                this.tb.Text = "";
                this.NumCmd.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Les données fournies semblent erronées .\n Veuillez ressayer", "Erreur", MessageBoxButton.OK);

                //    throw new NotImplementedException();
            }
        }


        private void Proceder(object sender, RoutedEventArgs e)
        {

            string str = (string)_comboSecteur0.SelectedItem;
            if (str != null && str != "" && l.Count != 0)
            {
                this.listCmd.WriteExcelFileV3(str);
            }
        }

        private void RetirerSec(object sender, EventArgs e)
        {
            int selectedIndex = _listboxNomSecteur.SelectedIndex;
            try
            {
                this.l.RemoveAt(selectedIndex); 
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
