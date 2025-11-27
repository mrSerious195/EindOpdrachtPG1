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
        public Klant(string voorNaam, string familienaam, Adress adress)
        {
            VoorNaam = voorNaam;
            Familienaam = familienaam;
            Adress = adress;
        }

        public Klant(int? id, string voorNaam, string familienaam, Adress adress)
        {
            Id = id;
            VoorNaam = voorNaam;
            Familienaam = familienaam;
            Adress = adress;
        }

        public int? Id {  get; set; }
        public string VoorNaam { get; set; }

        public string Familienaam { get; set; }

        public Adress Adress { get; set; }

        

    }
}
