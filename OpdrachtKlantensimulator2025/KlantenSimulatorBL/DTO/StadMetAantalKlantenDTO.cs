using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class StadMetAantalKlantenDTO
    {
        public string Stad {  get; set; }
        public int aantalKlanten {  get; set; }

        public StadMetAantalKlantenDTO(string stad, int aantalKlanten)
        {
            Stad = stad;
            this.aantalKlanten = aantalKlanten;
        }
    }
}
