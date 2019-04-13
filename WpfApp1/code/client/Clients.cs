using System;
using System.Collections.Generic;

namespace WpfApp1.code.client
{
    public class Clients
    {
        List<ClientBdd> list;
        public bool ReadCp(string text)
        {
            bool retunr = true;
            list = new List<ClientBdd>();
            string str = text;
            str = str.Replace('\r', ' ');
            string[] vs = str.Split('\n');

            if (vs.Length != 0)
            {
                for (int i = 1; i < vs.Length; i++)
                {
                    string line = vs[i];
                    try
                    {
                        if (!line.Equals(""))
                        {
                            Parameters p = Parameters.Instance();
                            string[] item = line.Split('\t');
                            ClientBdd art = new ClientBdd
                            {
                                IdClient = int.Parse(item[p.client.id - 1]),
                                Nom = item[p.client.nom - 1],
                                Prenom = item[p.client.prenom - 1],
                                Telephone1 = item[p.client.tel1 - 1],
                                Telephone2 = item[p.client.tel2 - 1],
                            };
                            bdd.Bdd.Instance().Addclient(art);
                        }
                    }
                    catch (Exception)
                    {
                        retunr = false;
                    }
                }
            }
            else { retunr = false; }
            return retunr;
        }

    }
}
