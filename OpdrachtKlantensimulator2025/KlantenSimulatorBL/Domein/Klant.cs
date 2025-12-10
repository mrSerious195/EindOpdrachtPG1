using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
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

        public Klant(uint? id, string voorNaam, string familienaam, Adress adress, Geslacht geslacht,DateTime geboorteDatum)
        {
            Id = id;
            VoorNaam = voorNaam;
            Familienaam = familienaam;
            Adress = adress;
            Geslacht= geslacht;
            GeboorteDatum = geboorteDatum;
        }

        public uint? Id {  get; set; }

        public string _voornaam;
        public string VoorNaam { get=> _voornaam; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new KlantenException("Kland.VoorNaam is null or empty");
                }
                else
                {
                    _voornaam = value;
                }

            }
        }
        public string _familienaam;
        public string Familienaam
        {
            get => _familienaam; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new KlantenException("Kland.Familienaam is null or empty");
                }
                else
                {
                    _familienaam = value;
                }

            }
        }

        private Adress _adress;
        public Adress Adress
        {
            get => _adress;
            set
            {
                if (_adress == null)
                {
                    throw new KlantenException("Klant.Adress is null");
                }
                else
                {
                    _adress = value;
                }
            }
        }

        public Geslacht Geslacht { get; set; }

        private DateTime _geboorteDatum;
        public DateTime GeboorteDatum {  get=> _geboorteDatum;
            set
            {
                if (value == default)
                {
                    throw new KlantenException("Klant.GeboorteDatum is default waarde");
                }
                else if(value>DateTime.Now)
                {
                    throw new KlantenException("Klant.GeboorteDatum is in de toekomst");
                }
                else
                {
                    _geboorteDatum = value;
                }
            }
        }
        

    }
}
