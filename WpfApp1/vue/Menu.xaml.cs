using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfApp1.code.bdd.cmdEmag;
using WpfApp1.code.bdd.cmdVlep;
using WpfApp1.code.bdd.NonAddresse;
using WpfApp1.code.client;

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
            ContentArea.Content = new UpdateBaseNA();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AjouterNA();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new PageEtiquette().ShowDialog();//Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            ContentArea.Content = new TestListView();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

            ContentArea.Content = new parametres();
        }



        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ListeManquants();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ListeNonAddresse();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Client();

        }
        public class KS
        {
            List<Key> Keys1 = new List<Key>{Key.Up, Key.Up,
                                       Key.Down, Key.Down,
                                       Key.Left,Key.Right,
                                       Key.Left, Key.Right,
                                       Key.B,Key.A};

            private int mPosition = -1;

            public int Position
            {
                get { return mPosition; }
                private set { mPosition = value; }
            }

            public bool IsCompletedBy(System.Windows.Input.Key key)
            {

                if (Keys1[Position + 1] == key)
                {
                    // move to next
                    Position++;
                }
                else if (Position == 1 && key == Key.Up)
                {
                    // stay where we are
                }
                else if (Keys1[0] == key)
                {
                    Position = 0;
                }
                else
                {
                    Position = -1;
                }
                if (Position == Keys1.Count - 1)
                {
                    Position = -1;
                    return true;
                }
                return false;
            }
        }

        private KS sequence = new KS();
        private void EmagMulti(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new CmdMulti();

        }
        private void VlepMulti(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PageVlep2();

        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sequence.IsCompletedBy(e.Key))
            {
                var q = new Window1();
                q.ShowDialog();
            }
        }
    }
}
