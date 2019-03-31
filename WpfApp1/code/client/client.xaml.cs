using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.code.bdd;

namespace WpfApp1.code.client
{
    /// <summary>
    /// Logique d'interaction pour client.xaml
    /// </summary>
    public partial class Client : UserControl
    {
        public Client()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
       //     if (_idClient.Text != "" && _nomClient.Text != "" && _prenomClient.Text != "")
         //   {
                items = Bdd.Instance().ListeClient(_idClient.Text, _nomClient.Text, _prenomClient.Text);
                lvUsers.ItemsSource = items;

           // }
        }
        List<ClientBdd> items = new List<ClientBdd>();
        private void LastNameCM_Click(object sender, RoutedEventArgs e)
        {

            lvUsers.Items.Refresh();
        }


    }
}
