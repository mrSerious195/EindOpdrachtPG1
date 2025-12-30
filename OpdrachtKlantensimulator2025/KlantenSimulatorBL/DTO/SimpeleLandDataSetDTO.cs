using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class SimpeleLandDataSetDTO
    {
        public int DataSetID{get;set;}
        public string DataSetNaam { get; set; }
        public string Land { get; set; }
        public int totaalAantalVoornamen { get; set; }
        public int totaalAantalAchterNamen { get; set; }
        public int totaalAantalSteden { get; set; }
        public int totaalAantalStraten { get; set; }
    }
}
