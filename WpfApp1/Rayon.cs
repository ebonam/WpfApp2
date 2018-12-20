using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Rayon
    {
        /*        LIQUIDE
        Epicerie
        DPH
        FLEG
        Nal
        Frais
        */
        public void fdf()
        {
            int i = 3;
            if (i == 25) {
                //fleg
            }
            if (i == 13 || i == 15)
            {
                //surg

            }
            if (i < 7 || i == 8 || i == 10)
            {
                //liquide
            }
            else if (i > 101)
            {
                //    Frais / boucherie

            }
            else if (i < 28 && i % 2 == 0)
            {
                //epi
            }
            else if (i <= 42 && i % 2 == 0)
            {
                //DPH

            }
            else if (i % 2 == 1 && i <= 23)
            {

            }
            else
            { //NAL}
            }
        }
    }
}
