

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
        public Stad Stad {  get; set; }
        public string Naam {  get; set; }

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