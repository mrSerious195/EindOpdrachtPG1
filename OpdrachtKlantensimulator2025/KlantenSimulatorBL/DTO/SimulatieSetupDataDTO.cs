using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class SimulatieSetupDataDTO
    {
        public int LandDataSetID;
        public int MinimumLeeftijd;
        public int MaximumLeeftijd;
        public int MaximumHuisNr;
        public int AantalMogelijkeLettersInHuisNr;
        public int TootaalAantalKlanten;
        public float KansVoorHuisNrMetLetter;
        public string Opdrachtgever;
        public int Seed;
        /// <summary>
        /// als deze leeg zou zijn worden de <TootaalAantalKlanten> verdeeld at random.
        /// </summary>
        public List<int>? GelimiteerdTotStedenStadIDs;

        public SimulatieSetupDataDTO(int seed,int landDataSetID, int minimumLeeftijd, int maximumLeeftijd, int maximumHuisNr, int aantalMogelijkeLettersInHuisNr, int tootaalAantalKlanten, float kansVoorHuisNrMetLetter, string opdrachtgever, List<int>? gelimiteerdTotStedenStadIDs)
        {
            Seed = seed;
            LandDataSetID = landDataSetID;
            MinimumLeeftijd = minimumLeeftijd;
            MaximumLeeftijd = maximumLeeftijd;
            MaximumHuisNr = maximumHuisNr;
            AantalMogelijkeLettersInHuisNr = aantalMogelijkeLettersInHuisNr;
            TootaalAantalKlanten = tootaalAantalKlanten;
            KansVoorHuisNrMetLetter = kansVoorHuisNrMetLetter;
            Opdrachtgever = opdrachtgever;
            GelimiteerdTotStedenStadIDs = gelimiteerdTotStedenStadIDs;
        }
    }
}
