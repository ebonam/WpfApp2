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
using WpfApp1.code.bdd.cmdEmag;
using WpfApp1.code.bdd.cmdVlep;

namespace WpfApp1.vue
{
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
            new CmdEmag().GetExcelFile();
        }
    }
}
