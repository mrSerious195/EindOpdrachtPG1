using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    internal class SimulatieSetupDataDTO
    {
        public int LandDataSetID;
        public int MinimumLeeftijd;
        public int MaximumLeeftijd;
        public int MaximumHuisNr;
        public int AantalMogelijkeLettersInHuisNr;
        public int TootaalAantalKlanten;
        public float KansVoorHuisNrMetLetter;
        /// <summary>
        /// als deze leeg zou zijn worden de <TootaalAantalKlanten> verdeeld at random.
        /// </summary>
        public HashSet<int>? GelimiteerdTotStedenStadIDs; 

    }
}
