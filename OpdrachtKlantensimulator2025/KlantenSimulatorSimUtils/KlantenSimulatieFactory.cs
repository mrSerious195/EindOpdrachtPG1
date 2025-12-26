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
            DataSetDBRepo repo = new(DatabankConectionString);
            LandDataSetManager landDataSetManager = new(repo);
            SimulatieGenerator gen = new SimulatieGenerator(landDataSetManager);
            SimulatieService service = new(gen);
            return service;

        }
    }
}
