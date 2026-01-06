using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO.NaamConstructerInfo
{
    public class ConstructorNaamJsonLezerDTO : IConstructorNaamLezerDTO
    {
        public ConstructorNaamJsonLezerDTO(string path, string jsonPropertyName)
        {
            Path = path;
            JsonPropertyName = jsonPropertyName;
        }

        public string Path { get; set; }

        public string JsonPropertyName { get; set; }

        public object Clone()
        {
           return new ConstructorNaamJsonLezerDTO(Path, JsonPropertyName);
        }
    }
}
