using KlantenSimulatorBL.Exceptions;
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

        public int Seed {  get; init; }
        private int _databaseID;
        public int DatasetID { get=> _databaseID; init
            {
                if ( value > 0)
                {
                    _databaseID = value;
                }
                else
                {
                    throw new KlantenException($"Simulatie.DatasetID: {value} is invalid");
                }
            }
        }

        private int? _id;
        public int? Id { get=> _id; 
            set {
                if (value == null || value > 0) 
                {
                    _id = value;
                }
                else
                {
                    throw new KlantenException($"Simulatie.Id: {value} is invalid");
                } 
            } 
        }
        public Land Land { get; init; }

        public int MinimumLeeftijd { get; private init; }

        public int MaximumLeeftijd { get; private init; }

        private float _percentageAdressenMetNummers;
        public float PercentageAdressenMetNummers { get=> _percentageAdressenMetNummers;  init
            {
                if (value <=1 && value >= 0)
                {
                    _percentageAdressenMetNummers = value;
                }
                else
                {
                    throw new KlantenException($"Simulatie.Id: {value} is invalid");
                }
            }
        }

        private List<Stad> _limitatieTotSteden;
        public IReadOnlyList<Stad> LimitatieTotSteden { get => _limitatieTotSteden; }

        private int _aantalMogelijkeLettersInHuisNr;
        public int AantalMogelijkeLettersInHuisNr { get => _aantalMogelijkeLettersInHuisNr;
            private set {
                if (value>=0&&value<=26)
                {
                    _aantalMogelijkeLettersInHuisNr = value;
                }
                else
                {
                    throw new KlantenException($"Simulatie.AantalMogelijkeLettersInHuisNr: {value} is invalid");
                }
                    
            }
        }

        private int _maxhuisnummer;
        public int MaxHuisnummer
        {
            get => _maxhuisnummer;
            private set
            {
                if (value > 0)
                {
                    _maxhuisnummer = value;
                }
                else
                {
                    throw new KlantenException($"Simulatie.MaxHuisnummer: {value} is invalid");
                }

            }
        }

        private string _opdrachtgever;
        public string OpdrachtGever
        {
            get=> _opdrachtgever;
            init
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "OpdrachtGever");
                _opdrachtgever=value;
            }
        }

        private List<Klant> _klanten;
        public IReadOnlyList<Klant> Klanten { get => _klanten; }

        public Simulatie(int seed,DateTime tijdVanGeneratie, int datasetID, int? id, Land land, int minimumLeeftijd, int maximumLeeftijd, float percentageAdressenMetNummers, List<Stad> limitatieTotSteden, int aantalMogelijkeLettersInHuisNr, int maxHuisnummer, string opdrachtGever, List<Klant> klanten)
        {
            Seed = seed;
            TijdVanGeneratie = tijdVanGeneratie;
            DatasetID = datasetID;
            Id = id;
            if (land == null)
            {
                throw new KlantenException("Simulatie.Land Land is null");
            }
            Land = land;
            MinimumLeeftijd = minimumLeeftijd;
            MaximumLeeftijd = maximumLeeftijd;

            PercentageAdressenMetNummers = percentageAdressenMetNummers;
            if (limitatieTotSteden == null)
            {
                throw new KlantenException("Simulatie.limitatieTotSteden limitatieTotSteden is null");

            }
            if (limitatieTotSteden.Contains(null))
                {
                    throw new KlantenException("Simulatie.limitatieTotSteden limitatieTotSteden contains null");
                }
                _limitatieTotSteden = limitatieTotSteden;
            AantalMogelijkeLettersInHuisNr = aantalMogelijkeLettersInHuisNr;
            MaxHuisnummer = maxHuisnummer;
            OpdrachtGever = opdrachtGever;
            if (klanten == null)
            {
                throw new KlantenException("Simulatie.klanten klanten is null");

            }
            if (klanten.Contains(null))
            {
                throw new KlantenException("Simulatie.klanten klanten contains null");
            }
            _klanten = klanten;
        }

        public Simulatie(int seed,int datasetID, Land land, int minimumLeeftijd, int maximumLeeftijd, float percentageAdressenMetNummers, List<Stad> limitatieTotSteden, int aantalMogelijkeLettersInHuisNr, int maxHuisnummer, string opdrachtGever, List<Klant> klanten)
            :this(seed,DateTime.Now,datasetID,null,land,minimumLeeftijd,maximumLeeftijd,percentageAdressenMetNummers,limitatieTotSteden,aantalMogelijkeLettersInHuisNr,maxHuisnummer,opdrachtGever,klanten)
        {
           
        }

    }
      

}
