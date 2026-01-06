using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO.NaamConstructerInfo
{
    public class ConstructorNaamTextLezerDTO : IConstructorNaamLezerDTO
    {
        public ConstructorNaamTextLezerDTO(string path, int naamColom, int frequencyColom, int starterRij,char seperator)
        {

            NaamColom = naamColom;
            FrequencyColom = frequencyColom;
            StarterRij = starterRij;
            Seperator = seperator;
            Path = path;
        }



        public int NaamColom { get; set; }
        public string Path { get; set; }
        public int FrequencyColom { get; set; }
        public int StarterRij { get; set; }

        public char Seperator {  get; set; }

        public object Clone()
        {
           return new ConstructorNaamTextLezerDTO(Path, NaamColom, FrequencyColom, StarterRij, Seperator);
        }
    }
}
