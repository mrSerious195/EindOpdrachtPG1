using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO
{
    public class ConstructorNaamJsonLezerDTO : IConstructorNaamLezerDTO
    {
        public ConstructorNaamJsonLezerDTO(string path, GramaticaalGeslacht geslacht, NaamSoort naamSoort, string jsonPropertyName)
        {
            Path = path;
            Geslacht = geslacht;
            NaamSoort = naamSoort;
            JsonPropertyName = jsonPropertyName;
        }

        public string Path { get; set; }
        public GramaticaalGeslacht Geslacht { get; set; }
        public NaamSoort NaamSoort { get; set; }

        public string JsonPropertyName { get; set; }

        public object Clone()
        {
           return new ConstructorNaamJsonLezerDTO(Path,Geslacht, NaamSoort, JsonPropertyName);
        }
    }
}
