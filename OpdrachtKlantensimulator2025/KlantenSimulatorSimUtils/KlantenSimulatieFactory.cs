using KlantenSimulatorBL.Generator;
using KlantenSimulatorBL.Managers;
using KlantenSimulatorBL.Services;
using KlantenSimulatorDLDatabank;

namespace KlantenSimulatorSimUtils
{
    public static class KlantenSimulatieFactory
    {
        public static SimulatieService geefServise(string DatabankConectionString)
        {
            LandDataSetDBRepo Landrepo = new(DatabankConectionString);
            KlantenSimulatieDBRepo simrepo = new(DatabankConectionString);
            LandDataSetManager landDataSetManager = new(Landrepo);
            SimulatieManager simulatieManager = new(simrepo);
            SimulatieGenerator gen = new SimulatieGenerator(landDataSetManager);
            SimulatieService service = new(gen, simulatieManager);
            return service;

        }
    }
}
