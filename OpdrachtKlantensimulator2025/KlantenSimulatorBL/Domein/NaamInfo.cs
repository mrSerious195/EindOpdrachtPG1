using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class NaamInfo
    {
        public NaamInfo(Land land, string naam, int frequency, GramatischGeslacht geslacht, bool isVoornaam)
        {
            Land = land;
            Naam = naam;
            Frequency = frequency;
            Geslacht = geslacht;
            IsVoornaam = isVoornaam;
        }

        public NaamInfo(int? id, Land land, string naam, int frequency, GramatischGeslacht geslacht, bool isVoornaam )
        {
            Id = id;
            Land = land;
            Naam = naam;
            Frequency = frequency;
            Geslacht = geslacht;
            IsVoornaam = isVoornaam;  
        }

        public int? Id {  get; set; }

        public Land Land { get; set; }
        public string Naam { get; set; }
        public int Frequency { get; set; }

        public GramatischGeslacht Geslacht { get; set; }

        public bool IsVoornaam { get; set; }

        public bool IsFamilienaam { get => !IsVoornaam; set { IsVoornaam = !value; } }



    }
}
