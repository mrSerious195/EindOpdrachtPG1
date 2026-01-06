using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class LandDataSet
    {
        public int? Id { get; set; }

        public Land Land { get; set; }

        public string NaamDataSet { get; set; }

        public bool ZijnAchternamenVMHetzelfde { get; set; }

        private List<NaamInfo> _voornamenM;

        public IReadOnlyList<NaamInfo> VoornamenM { get=> _voornamenM; }

        private List<NaamInfo> _voornamenV;
        public IReadOnlyList<NaamInfo> VoornamenV { get=> _voornamenV;  }

        private List<NaamInfo> _achternamenM;
        public IReadOnlyList<NaamInfo> AchternamenM { get=> _achternamenM;  }
        private List<NaamInfo> _achternamenV;
        public IReadOnlyList<NaamInfo> AchternamenV { get=> _achternamenV;  }
        private List<Straat> _straten;
        public IReadOnlyList<Straat> Straten { get=> _straten; }

        public LandDataSet(bool zijnAchternamenVMHetzelfde,string naamDataSet,Land land, List<NaamInfo> voornamenM, List<NaamInfo> voornamenV, List<NaamInfo> achternamenM, List<NaamInfo> achternamenV, List<Straat> straten)
        {
            ZijnAchternamenVMHetzelfde = zijnAchternamenVMHetzelfde;
            Land =land;
            _voornamenM = voornamenM;
            _voornamenV = voornamenV;
            _achternamenM = achternamenM;
            _achternamenV = achternamenV;
            _straten = straten;
            NaamDataSet = naamDataSet;
        }
        public LandDataSet(int id,bool zijnAchternamenVMHetzelfde, string naamDataSet, Land land, List<NaamInfo> voornamenM, List<NaamInfo> voornamenV, List<NaamInfo> achternamenM, List<NaamInfo> achternamenV, List<Straat> straten)
        {
            Id = id;
            ZijnAchternamenVMHetzelfde = zijnAchternamenVMHetzelfde;
            Land = land;
            _voornamenM = voornamenM;
            _voornamenV = voornamenV;
            _achternamenM = achternamenM;
            _achternamenV = achternamenV;
            _straten = straten;
            NaamDataSet = naamDataSet;
        }
    }
}
