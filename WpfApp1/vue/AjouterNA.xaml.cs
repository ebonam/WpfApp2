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
using System.Windows.Shapes;
using WpfApp1.code.bdd;
namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour AjouterNA.xaml
    /// </summary>
    public partial class AjouterNA : Window
    {
        public AjouterNA()
        {
            InitializeComponent();
            _Valider.Click += Ajouter;
            cancel.Click +=  listee;
        }

        private void listee(object sender, RoutedEventArgs e)
        {
            Bdd.Instance().listeNA();
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            Bdd.Instance().addNA(this._MC.Text.ToUpper(),  int.Parse( this._rayon.Text), (bool)this._bool.IsChecked, this._combo.SelectedIndex);
            Console.WriteLine(this._MC.Text.ToUpper()+"   " +int.Parse(this._rayon.Text)+"        " +(bool)this._bool.IsChecked+"   "+this._combo.SelectedIndex);
        }



    }
}
