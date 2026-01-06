using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IKlantenSimulatieRepo
    {
        public Task<int> SaveKlantenSimulatie(Simulatie sim);

        public Task<List<SimpeleSimulatieInfoDTO>> GeefSimpeleInfoVanSimulatiesAsync();
        public Task<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulatie(int simulatie_id);

        public Task<List<Klant>> GeefKlanten(int simulatie_id);

        public Task<DetailedSimulatieDTO> GeefDetailedInfoVanSimulatieAsync(int simulatie_id);

        public Task<List<StadMetAantalKlantenDTO>> GeefStadKlantenDistributieVanSimulatieAsync(int simulatie_id);

        public Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieVoornamenVanSimulatie(int simulatie_id, Geslacht geslacht);

        public Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieAchternamenVanSimulatie(int simulatie_id);

        public Task<KlantDTO> InfoJongsteKlant(int simulatie_id);

        public Task<KlantDTO> InfoOudsteKlant(int simulatie_id);
    }
}
