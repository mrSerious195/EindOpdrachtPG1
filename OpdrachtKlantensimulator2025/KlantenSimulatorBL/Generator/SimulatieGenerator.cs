using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;

namespace KlantenSimulatorBL.Generator
{
    public class SimulatieGenerator
    {
        private LandDataSetManager dataSetManager;

        public SimulatieGenerator(LandDataSetManager dataSetManager)
        {
            this.dataSetManager = dataSetManager;
        }

        public Simulatie generateSimulatie(SimulatieSetupDataDTO dataVoorSimulatie)
        {
            LandDataSet landData = dataSetManager.GetLandDataSet(dataVoorSimulatie.LandDataSetID);
          
            List<Straat> straten;
            HashSet<Stad> stedenlimitatie = new();
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
                        if (!stedenlimitatie.Contains(s.Stad))
                        {
                            stedenlimitatie.Add(s.Stad);
                        }
                    }

                }

            }
                AdressenGenerator adressenGenerator = new(straten,dataVoorSimulatie.KansVoorHuisNrMetLetter, dataVoorSimulatie.MaximumHuisNr, dataVoorSimulatie.AantalMogelijkeLettersInHuisNr);

            KlantenGenerator klantenGenerator = new(adressenGenerator, dataVoorSimulatie.MinimumLeeftijd, dataVoorSimulatie.MinimumLeeftijd, landData.VoornamenV, landData.VoornamenM, landData.AchternamenV, landData.AchternamenM);

            List<Klant> klanten = klantenGenerator.GenereerKlanten(dataVoorSimulatie.TootaalAantalKlanten);
            Simulatie simulatie = new(DateTime.Now,landData.Id.Value,null, landData.Land, dataVoorSimulatie.MinimumLeeftijd, dataVoorSimulatie.MaximumLeeftijd, dataVoorSimulatie.KansVoorHuisNrMetLetter, stedenlimitatie.ToList(), dataVoorSimulatie.AantalMogelijkeLettersInHuisNr, dataVoorSimulatie.MaximumHuisNr, dataVoorSimulatie.Opdrachtgever, klanten);
            return simulatie;
        }
    }
}