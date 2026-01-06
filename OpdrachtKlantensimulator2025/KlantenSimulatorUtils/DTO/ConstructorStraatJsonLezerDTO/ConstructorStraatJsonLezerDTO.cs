using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO.StadConstructorInfo
{
    public class ConstructorStraatJsonLezerDTO:IConstructorStreetLezerDTO
    {
        public string Path {  get; set; }

        public string DotPathSteden { get; set; }

        public string DotPathStraten { get; set; }

        public ConstructorStraatJsonLezerDTO(string path, string dotPathSteden, string dotPathStraten)
        {
            Path = path;
            DotPathSteden = dotPathSteden;
            DotPathStraten = dotPathStraten;
        }
    }
}
