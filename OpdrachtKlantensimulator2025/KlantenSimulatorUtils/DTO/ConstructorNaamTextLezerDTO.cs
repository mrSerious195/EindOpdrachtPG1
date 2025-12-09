using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO
{
    public class ConstructorNaamTextLezerDTO : IConstructorNaamLezerDTO
    {
        public ConstructorNaamTextLezerDTO(string path, GramaticaalGeslacht geslacht, NaamSoort naamSoort, int naamColom, int frequencyColom, int starterRij,char seperator)
        {
            Geslacht = geslacht;
            NaamSoort = naamSoort;
            NaamColom = naamColom;
            FrequencyColom = frequencyColom;
            StarterRij = starterRij;
            Seperator = seperator;
            Path = path;
        }

        public GramaticaalGeslacht Geslacht { get; set; }
        public NaamSoort NaamSoort { get; set; }

        public int NaamColom { get; set; }
        public string Path { get; set; }
        public int FrequencyColom { get; set; }
        public int StarterRij { get; set; }

        public char Seperator {  get; set; }

        public object Clone()
        {
           return new ConstructorNaamTextLezerDTO(Path,Geslacht, NaamSoort, NaamColom, FrequencyColom, StarterRij, Seperator);
        }
    }
}
