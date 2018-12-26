using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp1.code.bdd.cmdEmag;
using WpfApp1.code.bdd.cmdVlep;
using WpfApp1.code.bdd.NonAddresse;

namespace WpfApp1.vue
{
    //todo revoir tout ca pas beau
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PageVlep();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CmdEmagVue();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentArea.Content =  new UpdateBaseNA();
        
    }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AjouterNA();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
             new PageEtiquette().Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            ContentArea.Content = new TestListView();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
