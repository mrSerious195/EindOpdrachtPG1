

using KlantenSimulatorBL.Exceptions;

namespace KlantenSimulatorBL.Domein
{
    public class Straat
    {
        public Straat(Stad stad, string naam)
        {
            Stad = stad;
            Naam = naam;
        }

        public Straat(int? id, Stad stad, string naam)
        {
            Id = id;
            Stad = stad;
            Naam = naam;
        }

        public int? Id {  get; set; }

        private Stad stad;
        public Stad Stad {  get=>stad;
            set {
                if (value != null)
                {
                    stad = value;
                }
                else
                {
                    throw new KlantenException("Straat.Stad is null");
                }
            
            } }


        private string naam;
        public string Naam
        {
            get => naam;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new KlantenException("Straat.Naam is null or empty");
                }
                else
                {
                    naam = value;
                }

            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Straat straat &&
                   EqualityComparer<Stad>.Default.Equals(Stad, straat.Stad) &&
                   Naam == straat.Naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Stad, Naam);
        }
    }
}