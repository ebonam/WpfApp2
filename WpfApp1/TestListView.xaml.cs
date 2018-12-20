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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour TestListView.xaml
    /// </summary>
    public partial class TestListView : UserControl
    {
        List<Employe> items = new List<Employe>();
        public TestListView()
        {
            InitializeComponent();

            para = 0;
            items.Add(new Employe("franck", "ebel", "0", para++));
            
            items.Add(new Employe("frnck", "ebl", "1", para++));

           lvUsers.ItemsSource = items;
            
            Console.WriteLine("mes couilles");
        }
        private void LastNameCM_Click(object sender, RoutedEventArgs e)
        {
            items.Add(new Employe("franck", "ebel", "1", para++));
            lvUsers.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            items.RemoveAll(tr => (tr.nom == "SAM")); 
            lvUsers.Items.Refresh();
        }
        public int para;
    }
    public    class Employe
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string num { get; set; }
        public int para { get; set; }

    public Employe(string firstName, string lastName, string employeeNumber,int Para)
        {
            nom = firstName;
           prenom = lastName;
            num = employeeNumber;
            para = Para;
        }
    }
}
