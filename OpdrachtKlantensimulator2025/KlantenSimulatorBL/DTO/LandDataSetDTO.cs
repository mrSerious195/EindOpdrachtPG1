using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class LandDataSetDTO
    {
       public string Land {  get; set; }

        public StadDTO[] StadDTOs {  get; set; }

        public string[] VoornamenV { get; set; }
        public string[] VoornamenM { get; set; }
        public string[] AchternamenV { get; set; }
        public string[] AchternamenM { get; set; }
    }
}
