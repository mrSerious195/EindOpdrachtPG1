using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTO
{
    public class DetailedSimulatieDTO
    {
        public int Id { get; set; }
        public string OpdrachtGever { get; set; }
        public int SimulatieGrote { get; set; }
        public string NaamDataSet { get; set; }
        public string Land { get; set; }
        public DateTime MomentVanSimulatie { get; set; }

        public int Seed { get; set; }

        public int MinimumleeftijdSettings { get; set; }
        public int MaximumleeftijdSettings { get; set; }

 
        public int AantalMogelijkeLettersInHuisNummer {  get; set; }

        public float KansOpLettersInHuisNummer { get; set; }

        public float GemideldeSimulatieLeefrijdOpMomentVanAanMaak { get; set; }
        public float GemideldeSimulatieLeefrijdOpMomentVanOpvraagGegenens { get; set; }

        public List<string> GelimiteerdTotSteden {  get; set; }

        public DetailedSimulatieDTO(int id, string opdrachtGever, int simulatieGrote, string naamDataSet, string land, DateTime momentVanSimulatie, int seed, int minimumleeftijdSettings, int maximumleeftijdSettings, int aantalMogelijkeLettersInHuisNummer, float kansOpLettersInHuisNummer, float gemideldeSimulatieLeefrijdOpMomentVanAanMaak, float gemideldeSimulatieLeefrijdOpMomentVanOpvraagGegenens, List<string> gelimiteerdTotSteden)
        {
            Id = id;
            OpdrachtGever = opdrachtGever;
            SimulatieGrote = simulatieGrote;
            NaamDataSet = naamDataSet;
            Land = land;
            MomentVanSimulatie = momentVanSimulatie;
            Seed = seed;
            MinimumleeftijdSettings = minimumleeftijdSettings;
            MaximumleeftijdSettings = maximumleeftijdSettings;
            AantalMogelijkeLettersInHuisNummer = aantalMogelijkeLettersInHuisNummer;
            KansOpLettersInHuisNummer = kansOpLettersInHuisNummer;
            GemideldeSimulatieLeefrijdOpMomentVanAanMaak = gemideldeSimulatieLeefrijdOpMomentVanAanMaak;
            GemideldeSimulatieLeefrijdOpMomentVanOpvraagGegenens = gemideldeSimulatieLeefrijdOpMomentVanOpvraagGegenens;
            GelimiteerdTotSteden = gelimiteerdTotSteden;
        }
    }
}
