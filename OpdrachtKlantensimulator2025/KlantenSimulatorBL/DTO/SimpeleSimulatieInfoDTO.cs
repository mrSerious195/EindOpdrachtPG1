using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class SimpeleSimulatieInfoDTO
    {
        public int Id{get; set;}
        public string OpdrachtGever { get; set; }
        public int simulatieGrote { get; set; }
        public string NaamDataSet { get; set; }
        public string Land { get; set; }
        public DateTime MomentVanSimulatie { get; set; }

        public SimpeleSimulatieInfoDTO(int id, string opdrachtGever, int simulatieGrote, string naamDataSet, string land, DateTime momentVanSimulatie)
        {
            Id = id;
            OpdrachtGever = opdrachtGever;
            this.simulatieGrote = simulatieGrote;
            NaamDataSet = naamDataSet;
            Land = land;
            MomentVanSimulatie = momentVanSimulatie;
        }
    }
}
