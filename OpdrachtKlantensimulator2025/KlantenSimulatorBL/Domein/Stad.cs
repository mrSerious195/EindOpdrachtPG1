using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Stad
    {
        public int? id;

        public Land land;

        public string naam;

        public Stad(Land land, string naam)
        {
            this.land = land;
            this.naam = naam;
        }

        public Stad(int? id, Land land, string naam)
        {
            this.id = id;
            this.land = land;
            this.naam = naam;
        }

        public override bool Equals(object? obj)
        {
            return obj is Stad stad &&
                   EqualityComparer<Land>.Default.Equals(land, stad.land) &&
                   naam == stad.naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(land, naam);
        }
    }
}
