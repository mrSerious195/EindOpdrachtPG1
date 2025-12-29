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
    }
}
