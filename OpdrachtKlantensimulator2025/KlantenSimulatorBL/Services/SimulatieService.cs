using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Generator;
using KlantenSimulatorBL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Services
{
    public class SimulatieService
    {

        private SimulatieManager _simulatieManager;
        private SimulatieGenerator _simulatieGenerator;
        private OpdrachtGeverManager _opdrachtGeverManager;
        private LandDataSetManager _dataSetManager;


        public SimulatieService(OpdrachtGeverManager opdrachtGeverManager, LandDataSetManager dataSetManager, SimulatieManager simulatieManager)
        {
            _dataSetManager=dataSetManager;
            _simulatieGenerator = new SimulatieGenerator(dataSetManager);
            _simulatieManager = simulatieManager;
            _opdrachtGeverManager= opdrachtGeverManager;
        }

        public Simulatie genereerSimulatie(SimulatieSetupDataDTO data)
        {
            Simulatie sim = _simulatieGenerator.generateSimulatie(data);
            _simulatieManager.SaveSimulatie(sim);
            return sim;
        }


        public List<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulaties()
        {
            return _simulatieManager.GeefSimpeleInfoVanSimulaties();
        }

        public List<String> GeefAlleOpdrachtGevers()
        {
            return _opdrachtGeverManager.GeefAlleOpdrachtGevers();
        }
        public void VoegOpdrachtGeverToe(string naam)
        {
             _opdrachtGeverManager.VoegOpdrachtGeverToe(naam);
        }


        public List<SimpeleLandDataSetDTO> GeefSimpeleDataSetInfo()
        {
            return _dataSetManager.GeefSimpeleDataSetInfo();
        }

    }
}
