using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class KlantenDTO
    {
        public string Voornaam {  get; set; }
        public string AchterNaam { get; set; }

        public Geslacht Geslacht { get; set; }

        public string HuisNr {  get; set; }

        public string Straat { get; set; }

        public string Stad { get; set; }

        public string Land { get; set; }

        public DateTime GeboorteDatum {  get; set; }

        public KlantenDTO(string voornaam, string achterNaam, Geslacht geslacht, string huisNr, string straat, string stad, string land, DateTime geboorteDatum)
        {
            Voornaam = voornaam;
            AchterNaam = achterNaam;
            Geslacht = geslacht;
            HuisNr = huisNr;
            Straat = straat;
            Stad = stad;
            Land = land;
            GeboorteDatum = geboorteDatum;
        }
    }
}
