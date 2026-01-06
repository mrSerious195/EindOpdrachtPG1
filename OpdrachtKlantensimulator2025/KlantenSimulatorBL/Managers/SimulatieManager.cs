using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
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

        public async Task<int> SaveSimulatie(Simulatie sim)
        {
            return await _repo.SaveKlantenSimulatie(sim);
        }

        public async Task<List<SimpeleSimulatieInfoDTO>> GeefSimpeleInfoVanSimulatiesAsync()
        {
            return await _repo.GeefSimpeleInfoVanSimulatiesAsync();
        }

        public async Task<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulatieAsync(int simulatie_id)
        {
            return await _repo.GeefSimpeleInfoVanSimulatie(simulatie_id);
        }

        public async Task<DetailedSimulatieDTO> GeefDetailedInfoVanSimulatieAsync(int simulatie_id)
        {
            return await _repo.GeefDetailedInfoVanSimulatieAsync(simulatie_id);
        }

        public async Task<List<StadMetAantalKlantenDTO>> GeefStadKlantenDistributieVanSimulatieAsync(int simulatie_id)
        {
            return await _repo.GeefStadKlantenDistributieVanSimulatieAsync(simulatie_id);
        }

        public async Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieVoornamenVanSimulatie(int simulatie_id,Geslacht geslacht)
        {
            return await _repo.GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, geslacht);
        }

        public async Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieAchternamenVanSimulatie(int simulatie_id)
        {
            return await _repo.GeefKlantenDistributieAchternamenVanSimulatie(simulatie_id);
        }


        public async Task<KlantDTO> InfoOudsteKlant(int simulatie_id)
        {
            return await _repo.InfoOudsteKlant(simulatie_id);
        }

        public async Task<KlantDTO> InfoJongsteKlant(int simulatie_id)
        {
            return await  _repo.InfoJongsteKlant(simulatie_id);
        }


        public async Task<List<Klant>> GeefKlanten(int simulatie_id)
        {
            return await _repo.GeefKlanten(simulatie_id);
        }

    }
}
