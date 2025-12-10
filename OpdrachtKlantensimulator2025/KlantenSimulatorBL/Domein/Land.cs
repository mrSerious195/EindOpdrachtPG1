using KlantenSimulatorBL.Exceptions;
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
            this.Naam = naam;
        }

        public Land(uint? id, string naam)
        {
            this.Id = id;
            this.Naam = naam;
        }


        public uint? Id { get; set; }

        private string naam;
        public string Naam {  get=> naam; 
            set {
                if (string.IsNullOrEmpty(value)|| string.IsNullOrEmpty(value.Trim())) {
                    throw new KlantenException("Land.Naam is null or empty");
                }
                else
                {
                    naam = value;
                } 
            
            } }

        public override bool Equals(object? obj)
        {
            return obj is Land land &&
                   Naam == land.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Naam);
        }
    }
}
