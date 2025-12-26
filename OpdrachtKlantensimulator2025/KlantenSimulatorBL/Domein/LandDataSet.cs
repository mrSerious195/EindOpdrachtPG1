using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class LandDataSet
    {
        public int? Id;

        public Land Land;

        public string NaamDataSet;

        public bool ZijnAchternamenVMHetzelfde;
        public List<NaamInfo> VoornamenM;

        public List<NaamInfo> VoornamenV;

        public List<NaamInfo> AchternamenM;

        public List<NaamInfo> AchternamenV;

        public List<Straat> Straten;

        public LandDataSet(bool zijnAchternamenVMHetzelfde,string naamDataSet,Land land, List<NaamInfo> voornamenM, List<NaamInfo> voornamenV, List<NaamInfo> achternamenM, List<NaamInfo> achternamenV, List<Straat> straten)
        {
            ZijnAchternamenVMHetzelfde = zijnAchternamenVMHetzelfde;
            Land =land;
            VoornamenM = voornamenM;
            VoornamenV = voornamenV;
            AchternamenM = achternamenM;
            AchternamenV = achternamenV;
            Straten = straten;
            NaamDataSet = naamDataSet;
        }
        public LandDataSet(int id,bool zijnAchternamenVMHetzelfde, string naamDataSet, Land land, List<NaamInfo> voornamenM, List<NaamInfo> voornamenV, List<NaamInfo> achternamenM, List<NaamInfo> achternamenV, List<Straat> straten)
        {
            Id = id;
            ZijnAchternamenVMHetzelfde = zijnAchternamenVMHetzelfde;
            Land = land;
            VoornamenM = voornamenM;
            VoornamenV = voornamenV;
            AchternamenM = achternamenM;
            AchternamenV = achternamenV;
            Straten = straten;
            NaamDataSet = naamDataSet;
        }
    }
}
