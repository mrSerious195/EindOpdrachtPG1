using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class SimpeleLandDataSetDTO
    {
        public SimpeleLandDataSetDTO(int dataSetID, string dataSetNaam, string land, int aantalVoornamen, int aantalAchterNamen, int aantalSteden)
        {
            DataSetID = dataSetID;
            DataSetNaam = dataSetNaam;
            Land = land;
            AantalVoornamen = aantalVoornamen;
            AantalAchterNamen = aantalAchterNamen;
            AantalSteden = aantalSteden;
        }

        public int DataSetID{get;set;}
        public string DataSetNaam { get; set; }
        public string Land { get; set; }
        public int AantalVoornamen { get; set; }
        public int AantalAchterNamen { get; set; }
        public int AantalSteden { get; set; }

        public override string ToString()
        {
            return $"{DataSetNaam} ({Land} | {AantalSteden} steden | {AantalVoornamen} Voornamen | {AantalAchterNamen} achternamen)";
        }
    }
}
