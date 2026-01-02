using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
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

        public List<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulaties()
        {
            return _repo.GeefSimpeleInfoVanSimulaties();
        }

        public DetailedSimulatieDTO GeefDetailedInfoVanSimulatie(int simulatie_id)
        {
            return _repo.GeefDetailedInfoVanSimulatie(simulatie_id);
        }

        public List<StadMetAantalKlantenDTO> GeefStadKlantenDistributieVanSimulatie(int simulatie_id)
        {
            return _repo.GeefStadKlantenDistributieVanSimulatie(simulatie_id);
        }

        public List<NaamMetAantalKlantedDTO> GeefNaamKlantenDistributieVanSimulatie(int simulatie_id)
        {
            return _repo.GeefNaamKlantenDistributieVanSimulatie(simulatie_id);
        }

        public KlantenDTO InfoOudsteKlant(int simulatie_id)
        {
            return _repo.InfoOudsteKlant(simulatie_id);
        }

        public KlantenDTO InfoJongsteKlant(int simulatie_id)
        {
            return _repo.InfoJongsteKlant(simulatie_id);
        }

    }
}
