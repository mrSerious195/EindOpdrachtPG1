using KlantenSimulatorBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Stad
    {
        public uint? Id;

        private Land land;

        public Land Land
        {
            get => land;
            set
            {
                if (value==null)
                {
                    throw new KlantenException("Stad.Land is null");
                }
                else
                {
                    land = value;
                }

            }
        }


        private string naam;
        public string Naam
        {
            get => naam;
            set
            {
                if (string.IsNullOrEmpty(value)|| string.IsNullOrEmpty(value.Trim()))
                {
                    throw new KlantenException("Stad.Naam is null or empty");
                }
                else
                {
                    naam = value;
                }

            }
        }

        public Stad(Land land, string naam)
        {
            this.Land = land;
            this.Naam = naam;
        }

        public Stad(uint? id, Land land, string naam)
        {
            this.Id = id;
            this.Land = land;
            this.Naam = naam;
        }

        public override bool Equals(object? obj)
        {
            return obj is Stad stad &&
                   EqualityComparer<Land>.Default.Equals(Land, stad.Land) &&
                   Naam == stad.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Land, Naam);
        }
    }
}
