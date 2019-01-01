using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.vue
{
   
    /// <summary>
    /// Logique d'interaction pour pageEtiquettr.xaml
    /// </summary>
    public partial class PageEtiquette : Window
    {

        Etiquette etiquette0;
        Etiquette etiquette1;
        Etiquette etiquette2;
        Etiquette etiquette3;
        Etiquette etiquette4;
        Etiquette etiquette5;


        public PageEtiquette()
        {
            InitializeComponent();
            del0.Visibility = Visibility.Hidden;
            del1.Visibility = Visibility.Hidden;
            del2.Visibility = Visibility.Hidden;
            del3.Visibility = Visibility.Hidden;
            del4.Visibility = Visibility.Hidden;
            del5.Visibility = Visibility.Hidden;
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            foreach (string f in Directory.GetFiles(System.IO.Path.Combine(exeDir, "excel\\"), "*.png", SearchOption.TopDirectoryOnly))
            {
                File.Delete(f);
            }
        }

        private void Button_Ajouter(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(""+((Button)sender).Tag);
            this.cp.Visibility = Visibility.Visible;
            this.addtag.Tag = num;
        }

        private void Button_print(object sender, RoutedEventArgs e) {

            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintVisual(this.ToPrint, "Impression des étiquettes.");
            }
        }
        
            
            private void Button_Annuler(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(""+(((Button)sender).Tag));
            switch (num)
            {
                case 0:
                    etiquette0 = null;
                    del0.Visibility = Visibility.Hidden;
                    eti0.Content = etiquette0;
                    add0.Visibility = Visibility.Visible;
                    break;
                case 1:
                    etiquette1 = null;
                    del1.Visibility = Visibility.Hidden;
                    add1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    etiquette2 = null;
                    del2.Visibility = Visibility.Hidden;
                    add2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    etiquette3 = null;
                    del3.Visibility = Visibility.Hidden;
                    add3.Visibility = Visibility.Visible;
                    break;
                case 4:
                    etiquette4 = null;
                    del4.Visibility = Visibility.Hidden;
                    add4.Visibility = Visibility.Visible;
                    break;
                case 5:
                    etiquette5 = null;
                    del5.Visibility = Visibility.Hidden;
                    add5.Visibility = Visibility.Visible;
                    break;

            }
        }
    
        private void Button_AjouterEtiquette(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(""+((Button)sender).Tag);
            switch (num)
            {
                case 0:
                    etiquette0 = new Etiquette();
                    etiquette0.Setter(tb.Text);
                    add0.Visibility = Visibility.Hidden;
                    eti0.Content = etiquette0;
                    del0.Visibility = Visibility.Visible;
                    break;
                case 1:
                    etiquette1 = new Etiquette();
                    etiquette1.Setter(tb.Text);
                    add1.Visibility = Visibility.Hidden;
                    eti1.Content = etiquette1;
                    del1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    etiquette2 = new Etiquette();
                    etiquette2.Setter(tb.Text);
                    add2.Visibility = Visibility.Hidden;
                    eti2.Content = etiquette2;
                    del2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    etiquette3 = new Etiquette();
                    etiquette3.Setter(tb.Text);
                    add3.Visibility = Visibility.Hidden;
                    eti3.Content = etiquette3;
                    del3.Visibility = Visibility.Visible; break;
                case 4:
                    etiquette4 = new Etiquette();
                    etiquette4.Setter(tb.Text);
                    add4.Visibility = Visibility.Hidden;
                    eti4.Content = etiquette4;
                    del4.Visibility = Visibility.Visible;
                    break;
                case 5:
                    etiquette5 = new Etiquette();
                    etiquette5.Setter(tb.Text);
                    add0.Visibility = Visibility.Hidden;
                    eti5.Content = etiquette5;
                    del5.Visibility = Visibility.Visible;
                    break;
            }
            this.tb.Text = "";
            this.cp.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
           
            eti1.Content = null;
            eti2.Content = null;
            eti3.Content = null;
            eti4.Content = null;
            eti5.Content = null;
            eti0.Content = null;
            etiquette0 = null;
            etiquette1 = null;
            etiquette2 = null;
            etiquette3 = null;
            etiquette4 = null;
            etiquette5 = null;
//todo regler cette merde
            
        }
    }
}
