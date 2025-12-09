using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorInitialisationUtils.DTO
{
    public interface IConstructorNaamLezerDTO: ICloneable
    {
        public string Path {  get; set; }
        public GramaticaalGeslacht Geslacht { get; set; }
        public NaamSoort NaamSoort { get; set; }



    }
}
