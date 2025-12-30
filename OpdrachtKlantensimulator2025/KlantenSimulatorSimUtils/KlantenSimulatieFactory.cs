using KlantenSimulatorBL.Generator;
using KlantenSimulatorBL.Managers;
using KlantenSimulatorBL.Services;
using KlantenSimulatorDLDatabank;

namespace KlantenSimulatorSimUtils
{
    public static class KlantenSimulatieFactory
    {
        public static SimulatieService GeefServise(string DatabankConectionString)
        {
            LandDataSetDBRepo Landrepo = new(DatabankConectionString);
            KlantenSimulatieDBRepo simrepo = new(DatabankConectionString);
            LandDataSetManager landDataSetManager = new(Landrepo);
            SimulatieManager simulatieManager = new(simrepo);
            OpdrachtGeversDBRepo opdrachtGeversDBRepo = new(DatabankConectionString);
            OpdrachtGeverManager opdrachtGeverManager = new(opdrachtGeversDBRepo);
            SimulatieService service = new(opdrachtGeverManager, landDataSetManager, simulatieManager);
            return service;

        }
    }
}
