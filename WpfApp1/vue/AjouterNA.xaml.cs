using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.code.bdd;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour AjouterNA.xaml
    /// </summary>
    public partial class AjouterNA : UserControl
    {
        public AjouterNA()
        {
            InitializeComponent();
            _Valider.Click += Ajouter;
            cancel.Click += Listee;
        }

        private void Listee(object sender, RoutedEventArgs e)
        {
            Bdd.Instance().ListeNA();
        }

        private void Ajouter(object sender, RoutedEventArgs e)
        {
            Bdd.Instance().AddNA(this._MC.Text.ToUpper(), int.Parse(this._rayon.Text), (bool)this._bool.IsChecked, this._combo.SelectedIndex);
            Console.WriteLine(this._MC.Text.ToUpper() + "   " + int.Parse(this._rayon.Text) + "        " + (bool)this._bool.IsChecked + "   " + this._combo.SelectedIndex);
        }
    }
}
