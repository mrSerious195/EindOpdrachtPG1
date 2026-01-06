using KlantenSimulatorInitialisationUtils.DTO.StadConstructorInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO.ConstructorStraatJsonLezerDTO
{
    public class ConstructorStraatTXTLezerDTO: IConstructorStreetLezerDTO
    {
        public string Path { get; set; }

        public ConstructorStraatTXTLezerDTO(string path)
        {
            Path = path;
        }
    }
}
