using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Adress
    {
        public Adress(string huisNr, Straat straat)
        {
            HuisNr = huisNr;
            Straat = straat;
        }

        public string HuisNr {  get; set; }
        public Straat Straat { get; set; }


    }
}
