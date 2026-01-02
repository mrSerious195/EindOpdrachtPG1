using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Generator;
using KlantenSimulatorBL.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        private LandDataSetManager _landDataSetManager;


        public SimulatieService(OpdrachtGeverManager opdrachtGeverManager, LandDataSetManager dataSetManager, SimulatieManager simulatieManager)
        {
            _landDataSetManager=dataSetManager;
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
            return _landDataSetManager.GeefSimpeleDataSetInfo();
        }

        public List<StadDTO> GeefInfoSteden(int datasetID)
        {
            return _landDataSetManager.GeefInfoSteden(datasetID);
        }

        public DetailedSimulatieDTO GeefConfigInfoVanSimulatie(int simulatie_id)
        {
            return _simulatieManager.GeefDetailedInfoVanSimulatie(simulatie_id);
        }

        public List<StadMetAantalKlantenDTO> GeefStadKlantenDistributieVanSimulatie(int simulatie_id)
        {
            return _simulatieManager.GeefStadKlantenDistributieVanSimulatie(simulatie_id);
        }

        public List<NaamMetAantalKlantedDTO> GeefNaamKlantenDistributieVanSimulatie(int simulatie_id)
        {
            return _simulatieManager.GeefNaamKlantenDistributieVanSimulatie(simulatie_id);
        }

        public KlantenDTO InfoOudsteKlant(int simulatie_id)
        {
            return _simulatieManager.InfoOudsteKlant(simulatie_id);
        }

        public KlantenDTO InfoJongsteKlant(int simulatie_id)
        {
            return _simulatieManager.InfoJongsteKlant(simulatie_id);
        }


    }
}
