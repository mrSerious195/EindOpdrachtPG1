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
        public int? id { get; init; }
        public Land Land { get; init; }

        public int MinimumLeeftijd;
        public int MaximumLeeftijd;
        public float PercentageAdressenMetNummers;
        public List<Stad> LimitatieTotSteden;
        public int AantalMogelijkeLettersInHuisNr;
        public int MaxHuisnummer;
        public string OpdrachtGever;
        private List<Klant> _klanten;
        public IReadOnlyList<Klant> Klanten { get => _klanten; }
        // Aantal Straten Per Gemeente ???
        public int TootaalAantalKlanten;
        public Klant JongsteKlant;
        public Klant OudsteKlant;
        public float GemiddeldeLeeftijdKlantOpGeneratie;
        public float GemiddeldeLeeftijdKlantNu;


        public Dictionary<Stad, int> _aantalKlantenPerStad;
        public IReadOnlyDictionary<Stad, int> AantalKlantenPerStad { get => _aantalKlantenPerStad; }
        public Simulatie(DateTime tijdVanGeneratie, int datasetID, int? id, Land land, int minimumLeeftijd, int maximumLeeftijd, float percentageAdressenMetNummers, List<Stad> limitatieTotSteden, int aantalMogelijkeLettersInHuisNr, int maxHuisnummer, string opdrachtGever, List<Klant> klanten)
        {
            TijdVanGeneratie = tijdVanGeneratie;
            DatasetID = datasetID;
            this.id = id;
            Land = land;
            MinimumLeeftijd = minimumLeeftijd;
            MaximumLeeftijd = maximumLeeftijd;
            PercentageAdressenMetNummers = percentageAdressenMetNummers;
            LimitatieTotSteden = limitatieTotSteden;
            AantalMogelijkeLettersInHuisNr = aantalMogelijkeLettersInHuisNr;
            MaxHuisnummer = maxHuisnummer;
            OpdrachtGever = opdrachtGever;
            _klanten = klanten;
        }


    }
      

}
