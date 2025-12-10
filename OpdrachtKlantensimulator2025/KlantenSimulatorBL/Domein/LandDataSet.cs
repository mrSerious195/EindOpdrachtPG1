using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class LandDataSet
    {
        uint? Id;
        public List<NaamInfo> VoornamenM;

        public List<NaamInfo> VoornamenV;

        public List<NaamInfo> AchternamenM;

        public List<NaamInfo> AchternamenV;

        public List<Straat> Straten;

        public LandDataSet( List<NaamInfo> voornamenM, List<NaamInfo> voornamenV, List<NaamInfo> achternamenM, List<NaamInfo> achternamenV, List<Straat> straten)
        {
            VoornamenM = voornamenM;
            VoornamenV = voornamenV;
            AchternamenM = achternamenM;
            AchternamenV = achternamenV;
            Straten = straten;
        }
    }
}
