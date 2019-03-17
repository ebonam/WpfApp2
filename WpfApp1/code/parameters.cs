using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.code
{
    class Parameters
    {

        public class defrayon {
            public string nom;
            public int min;
            public int max;
            public bool paire;
            public List<string> rayon; 

        }

        public List<defrayon> ps;


        public bool vlepprintdirect;
        public bool Emagprintdirect;
        public bool Naprintdirect;
        public List<string> TGs;
        public string datelastUpdateBase;









        private static Parameters _instance = null;

        private Parameters()
        {

        }

        public static Parameters Instance()
        {
            if (_instance == null)
                _instance = new Parameters();
            return _instance;
        }
    }
}
