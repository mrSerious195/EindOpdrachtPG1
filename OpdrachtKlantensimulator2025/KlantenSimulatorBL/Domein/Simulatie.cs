using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Domein
{
    public class Simulatie
    {
        public DateTime TijdVanGeneratie { get; init; }
        public int DatasetID { get; init; }
        public int? id {  get; init; }
        public Land Land { get; init; }

        public int MinimumLeeftijd;
        public int MaximumLeeftijd;
        public int TootaalAantalKlanten;
        public bool KunnenAdressenNummersHebben;
        // Aantal Straten Per Gemeente ???
        public String OpdrachtGever;
        public Klant JongsteKlant;
        public Klant OudsteKlant;
        public float GemiddeldeLeeftijdKlantOpGeneratie;
        public float GemiddeldeLeeftijdKlantNu;


        public Dictionary<Stad, int> _aantalKlantenPerStad;
        public IReadOnlyDictionary<Stad, int> AantalKlantenPerStad { get=> _aantalKlantenPerStad; }

        private List<Klant> _klanten;
        public IReadOnlyList<Klant> Klanten { get=> _klanten;}

      
public Simulatie(int datasetID, Land land, string opdrachtGever)
        {
            OpdrachtGever = opdrachtGever;
            DatasetID = datasetID;
            Land = land;
            _klanten = new();
            _aantalKlantenPerStad = new();
        }
    }
}
