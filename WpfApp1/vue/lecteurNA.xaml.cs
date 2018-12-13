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
using WpfApp1.code.bdd.cmdVlep;

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour lecteurNA.xaml
    /// </summary>
    public partial class LecteurNA : Page
    {
        public LecteurNA()
        {
            InitializeComponent();
            b.Click += Te;
        }

        private void Te(object sender, RoutedEventArgs e)
        {
         //  VlepCmd.Test( sdsf.Text);
        }
    }
}
