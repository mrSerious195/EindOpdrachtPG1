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
       // private SimulatieManager simulatieManager;
        private SimulatieGenerator _simulatieGenerator;


        public SimulatieService(SimulatieGenerator simulatieGenerator)
        {
            _simulatieGenerator = simulatieGenerator;
        }

        public Simulatie genereerSimulatie(SimulatieSetupDataDTO data)
        {
            return _simulatieGenerator.generateSimulatie(data);
        }


    }
}
