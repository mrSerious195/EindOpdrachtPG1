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

        public int AantalKlantenMetNaam {  get; set; }

        public NaamMetAantalKlantedDTO(string naam, int aantalKlanten)
        {
            Naam = naam;
         
            AantalKlantenMetNaam = aantalKlanten;
        }
    }
}
