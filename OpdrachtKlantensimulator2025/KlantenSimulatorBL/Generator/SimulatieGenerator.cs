using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;

namespace KlantenSimulatorBL.Generator
{
    internal class SimulatieGenerator
    {
        private LandDataSetManager dataSetManager;

        public Simulatie generateSimulatie(SimulatieSetupDataDTO dataVoorSimulatie)
        {
            LandDataSet landData = dataSetManager.GetLandDataSet(dataVoorSimulatie.LandDataSetID);
            List<Straat> straten;
            if (dataVoorSimulatie.GelimiteerdTotStedenStadIDs.Count == 0)
            {
                straten = landData.Straten.ToList();
            }
            else
            {
                straten = new();
                foreach( Straat s in landData.Straten)
                {
                    if (dataVoorSimulatie.GelimiteerdTotStedenStadIDs.Contains(s.Stad.Id.Value))
                    {
                        straten.Add(s);
                    }

                }

            }
                AdressenGenerator adressenGenerator = new(straten,dataVoorSimulatie.KansVoorHuisNrMetLetter, dataVoorSimulatie.MaximumHuisNr, dataVoorSimulatie.AantalMogelijkeLettersInHuisNr);
                
                
        
        
        }
    }
}