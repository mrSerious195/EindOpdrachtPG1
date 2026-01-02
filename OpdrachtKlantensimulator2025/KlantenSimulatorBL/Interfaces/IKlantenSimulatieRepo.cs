using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IKlantenSimulatieRepo
    {
        public void SaveKlantenSimulatie(Simulatie sim);

        public List<SimpeleSimulatieInfoDTO> GeefSimpeleInfoVanSimulaties();

        public DetailedSimulatieDTO GeefDetailedInfoVanSimulatie(int simulatie_id);

        public List<StadMetAantalKlantenDTO> GeefStadKlantenDistributieVanSimulatie(int simulatie_id);

        public List<NaamMetAantalKlantedDTO> GeefNaamKlantenDistributieVanSimulatie(int simulatie_id);

        public KlantenDTO InfoJongsteKlant(int simulatie_id);

        public KlantenDTO InfoOudsteKlant(int simulatie_id);
    }
}
