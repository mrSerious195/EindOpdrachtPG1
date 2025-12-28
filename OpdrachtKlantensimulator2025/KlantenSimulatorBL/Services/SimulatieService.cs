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
      //  private LandDataSetManager landDataSetManager;
        private SimulatieManager _simulatieManager;
        private SimulatieGenerator _simulatieGenerator;


        public SimulatieService(SimulatieGenerator simulatieGenerator, SimulatieManager simulatieManager)
        {
            _simulatieGenerator = simulatieGenerator;
            _simulatieManager = simulatieManager;
        }

        public Simulatie genereerSimulatie(SimulatieSetupDataDTO data)
        {
            Simulatie sim = _simulatieGenerator.generateSimulatie(data);
            _simulatieManager.SaveSimulatie(sim);
            return sim;
        }


    }
}
