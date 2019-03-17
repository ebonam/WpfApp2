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
using WpfApp1.code.bdd;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour TestListView.xaml
    /// </summary>
    public partial class TestListView : UserControl
    {
        List<NA> items = new List<NA>();
        public TestListView()
        {
            InitializeComponent();

            para = 0;
        items=    Bdd.Instance().ListeNA();
           lvUsers.ItemsSource = items;
            
          
        }
        private void LastNameCM_Click(object sender, RoutedEventArgs e)
        {
           
            lvUsers.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = int.Parse(((Button)sender).Tag + "");

            Bdd.Instance().RemoveNA(items.Find(tr=> tr._Id==i));
            items.RemoveAll(tr => (tr._Id == i)); 
            lvUsers.Items.Refresh();
        }
        public int para;
    }
    
    
}