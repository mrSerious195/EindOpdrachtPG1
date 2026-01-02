using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class NaamMetAantalKlantedDTO
    {
        public string Naam { get; set; }

        public NaamSoort NaamSoort { get; set; }

        public int AantalKlanten {  get; set; }

        public NaamMetAantalKlantedDTO(string naam, NaamSoort naamSoort, int aantalKlanten)
        {
            Naam = naam;
            NaamSoort = naamSoort;
            AantalKlanten = aantalKlanten;
        }
    }
}
