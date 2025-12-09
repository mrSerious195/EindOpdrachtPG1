using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class LandDataSet
    {
        public List<NaamInfo> VoornamenM;

        public List<NaamInfo> VoornamenV;

        public List<NaamInfo> AchterNamenM;

        public List<NaamInfo> AchterNamenV;

        public List<Straat> Straten;

        public LandDataSet( List<NaamInfo> voornamenM, List<NaamInfo> voornamenV, List<NaamInfo> achterNamenM, List<NaamInfo> achterNamenV, List<Straat> straten)
        {
            VoornamenM = voornamenM;
            VoornamenV = voornamenV;
            AchterNamenM = achterNamenM;
            AchterNamenV = achterNamenV;
            Straten = straten;
        }
    }
}
