using KlantenSimulatorBL.Generator;
using KlantenSimulatorBL.Managers;
using KlantenSimulatorBL.Services;
using KlantenSimulatorDLDatabank;
using KlantenSimulatorDLExporters;

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
            KlantenSimulatieExporter exporter = new();
            SimulatieService service = new(exporter,opdrachtGeverManager, landDataSetManager, simulatieManager);
            return service;

        }
    }
}
