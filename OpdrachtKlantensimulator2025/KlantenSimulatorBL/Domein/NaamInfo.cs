using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
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

        public NaamInfo(int? id, string naam, int frequency, GramaticaalGeslacht geslacht, NaamSoort naamsoort )
        {
            Id = id;
      
            Naam = naam;
            Frequency = frequency;
            Geslacht = geslacht;
            NaamSoort = naamsoort;  
        }

        public int? Id {  get; set; }

        private string _naam;
        public string Naam
        {
            get => _naam;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()))
                {
                    throw new KlantenException("NaamInfo.Naam is null or empty");
                }
                else
                {
                    _naam = value;
                }

            }
        }

        private int _frequency { get; set; }
        public int Frequency { get=> _frequency; set
            {
                if (value > 0)
                {
                    _frequency = value;
                }
                else
                {
                    throw new KlantenException("NaamInfo.Frequency is kleiner dan 1");
                }
            }
        }

        public GramaticaalGeslacht Geslacht { get; set; }

        public NaamSoort NaamSoort { get; set; }

       



    }
}
