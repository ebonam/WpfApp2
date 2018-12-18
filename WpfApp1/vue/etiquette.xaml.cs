using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WpfApp1.vue
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Etiquette : UserControl
    {
        //todo voir pour le barcode
        public Etiquette()
        {
           
            InitializeComponent();
        /*    PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("C:\\Path To\\PALETX3.ttf");
            label1.FontStyle = pfc[0]; new System.Windows.FontStyle();// new Font(pfc.Families[0], 16, System.Drawing.FontStyle.Regular);
            */





            //Create your private font collection object.
            PrivateFontCollection pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = Properties.Resources.code128.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.code128;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);
            label1.Text = "C002460030139357";
            label1.FontFamily = new FontFamily(new Uri(@"C:\Users\antoine\Downloads\code128.ttf"),"code 128");
           ///

        }
    }
}
