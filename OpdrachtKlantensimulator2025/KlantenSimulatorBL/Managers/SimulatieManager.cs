using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Managers
{
    public class SimulatieManager
    {
        private IKlantenSimulatieRepo _repo;
        public SimulatieManager(IKlantenSimulatieRepo repo) {
            _repo = repo;

        }

        public void SaveSimulatie(Simulatie sim)
        {
            _repo.SaveKlantenSimulatie(sim);
        }
    }
}
