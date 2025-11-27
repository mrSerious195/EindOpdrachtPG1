using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Land
    {

        public Land(string naam)
        {
            this.naam = naam;
        }

        public Land(int? id, string naam)
        {
            this.id = id;
            this.naam = naam;
        }


        public int? id;
        public string naam;

        public override bool Equals(object? obj)
        {
            return obj is Land land &&
                   naam == land.naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(naam);
        }
    }
}
