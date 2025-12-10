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
        public NaamInfo(string naam, int frequency, GramaticaalGeslacht geslacht, NaamSoort naamsoort)
        {

            Naam = naam;
            Frequency = frequency;
            Geslacht = geslacht;
            NaamSoort = naamsoort;
        }

        public NaamInfo(uint? id, string naam, int frequency, GramaticaalGeslacht geslacht, NaamSoort naamsoort )
        {
            Id = id;
      
            Naam = naam;
            Frequency = frequency;
            Geslacht = geslacht;
            NaamSoort = naamsoort;  
        }

        public uint? Id {  get; set; }
        public string Naam { get; set; }
        public int Frequency { get; set; }

        public GramaticaalGeslacht Geslacht { get; set; }

        public NaamSoort NaamSoort { get; set; }

       



    }
}
