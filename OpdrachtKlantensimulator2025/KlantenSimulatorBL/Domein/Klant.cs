using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Klant
    {
        public Klant(string voorNaam, string familienaam, Adress adress, Geslacht geslacht, DateTime geboorteDatum)
        {
            VoorNaam = voorNaam;
            Familienaam = familienaam;
            Adress = adress;
            Geslacht = geslacht;
            GeboorteDatum = geboorteDatum;
        }

        public Klant(int? id, string voorNaam, string familienaam, Adress adress, Geslacht geslacht,DateTime geboorteDatum)
        {
            Id = id;
            VoorNaam = voorNaam;
            Familienaam = familienaam;
            Adress = adress;
            Geslacht= geslacht;
            GeboorteDatum = geboorteDatum;
        }

        public int? Id {  get; set; }
        public string VoorNaam { get; set; }

        public string Familienaam { get; set; }

        public Adress Adress { get; set; }

        public Geslacht Geslacht { get; set; }

        public DateTime GeboorteDatum {  get; set; }
        

    }
}
