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
    /// Logique d'interaction pour PageVlep2.xaml
    /// </summary>
    public partial class PageVlep2 : UserControl
    {
        List<VlepCmd> vlepCmds=new List<VlepCmd>();
        Parameters p;
        List<string> l;
        List<string> ListeCMD;
        public PageVlep2()
        {
            l = new List<string>();
            ListeCMD = new List<string>();
            p = Parameters.Instance();
            InitializeComponent();
            
            _comboSecteur0.ItemsSource = l;
            
            _listboxNomSecteur.ItemsSource = p.ps.nomSecteur;

            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            VlepCmd vlepCmd = new VlepCmd();
            vlepCmd.Test(textblock.Text);
            vlepCmd.WriteExcelFile();
        }

        private void AfficherRayonSecteur(object sender, RoutedEventArgs e)
        {

            string str = (string)_comboSecteur0.SelectedItem;
            if (str != null && str != "")
            {
                           }
        }

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
            
        }
        private void UpdateSelector()
        {
            _comboSecteur0.ItemsSource = null;
            _comboSecteur0.ItemsSource = p.ps.nomSecteur;
        }




    }
}
