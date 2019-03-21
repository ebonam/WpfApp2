namespace WpfApp1.code.bdd.cmdEmag
{
    public class ArticleEmag
    {
        public string _ean;
        public string _lib;
        public string _qte;
        public string _prix;
        public string _loc;
        public string _sec;
        public int rayon;
        public int trave;
        public void SetSec()
        {
            Parameters p = Parameters.Instance();
           
            string[] s = _loc.Split('.');
            rayon = int.Parse(s[0]);
            trave = int.Parse(s[1]);
            foreach (Parameters.Defrayon sec in p.ps.secteurs)
            {
                if (sec.rayon.Contains(""+rayon))
                {
                    _sec = sec.nom;
                    return ;
                }
            }
            _sec = "NA";
            return ;

        
    }


    public ArticleEmag()
        {
        }
    }
}