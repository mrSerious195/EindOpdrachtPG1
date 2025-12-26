using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Generator
{
    internal class KlantenGenerator
    {
        private int _minimumleeftijd;
        private int _maximumleeftijd;

        private int _sumFrequencyVoornamenV;
        private List<(NaamInfo,int)> _voornamenVBinaryList;
        private int _sumFrequencyVoornamenM;
        private List<(NaamInfo,int)> _voornamenMBinaryList;
        private int _sumFrequencyAchternamenV;
        private List<(NaamInfo,int)> _achternamenVBinaryList;
        private int _sumFrequencyAchternamenM;
        private List<(NaamInfo,int)> _achternamenMBinaryList;
  

        private AdressenGenerator _adressenGenerator;
        public KlantenGenerator(AdressenGenerator adressenGenerator, int minimumleeftijd, int maximumleeftijd, List<NaamInfo> voornamenV, List<NaamInfo> voornamenM, List<NaamInfo> achternamenV, List<NaamInfo> achternamenM)
        {
            _minimumleeftijd = minimumleeftijd;
            _maximumleeftijd = maximumleeftijd;
            _voornamenVBinaryList = NaamInfoListToBinaryTable(voornamenV, out _sumFrequencyVoornamenV);
            _voornamenMBinaryList = NaamInfoListToBinaryTable(voornamenM, out _sumFrequencyVoornamenM);
            _achternamenVBinaryList = NaamInfoListToBinaryTable(achternamenV,out _sumFrequencyAchternamenV);
            _achternamenMBinaryList = NaamInfoListToBinaryTable(achternamenM, out _sumFrequencyAchternamenM);
            _adressenGenerator = adressenGenerator;
        }

        private List<(NaamInfo, int)> NaamInfoListToBinaryTable(List<NaamInfo> naaminfos, out int lijstsom)
        {
            lijstsom = 0;
            List <(NaamInfo,int)> binaryTable = new();

            foreach (NaamInfo n in naaminfos) {
                lijstsom += n.Frequency;
                binaryTable.Add((n,lijstsom));
            }

            return binaryTable;
        }

        private NaamInfo GeefRandomNaam(List<(NaamInfo, int)> naamBinaryList,int sumFrequancy)
        {
            int lowerBound = 0;
            int upperBound = sumFrequancy;
            int lowerIndex = 0;
            int upperIndex = naamBinaryList.Count;
            int lookForSumF = Random.Shared.Next(sumFrequancy)+1;
            int currentIndex = naamBinaryList.Count / 2;
            int currentSumF = naamBinaryList[currentIndex].Item2;

            // zoeken waar het random getal zich bevind met binary search
            while(lowerIndex != upperIndex)
            {
                if(lookForSumF> currentSumF)
                {
                    lowerIndex = currentIndex;
                    lowerBound = currentSumF;
                }
                else if(lookForSumF < currentSumF)
                {
                    upperIndex = currentIndex;
                    upperBound = currentSumF;
                }
                else
                {
                    break;
                }

                currentIndex = (upperIndex + lowerIndex) / 2;
                currentSumF= naamBinaryList[currentIndex].Item2;
            }

            return naamBinaryList[currentIndex].Item1;


        }

        public List<Klant> GenereerKlanten(int hoeveelKlanten)
        {
            List<Klant> klanten=new();
            Geslacht[] alleGeslachten = (Geslacht[]) Enum.GetValues(typeof(Geslacht));
            int leeftijdDiff = _maximumleeftijd - _minimumleeftijd;
            for (int i = 0; i < hoeveelKlanten; i++) {
                Geslacht geslacht= alleGeslachten[Random.Shared.Next(alleGeslachten.Length)];
                string voornaam, achternaam;
                switch(geslacht){
                    case Geslacht.Man:
                        voornaam = GeefRandomNaam(_voornamenMBinaryList, _sumFrequencyVoornamenM).Naam;
                        achternaam = GeefRandomNaam(_achternamenMBinaryList, _sumFrequencyAchternamenM).Naam;
                        break;
                    case Geslacht.Vrouw:
                        voornaam = GeefRandomNaam(_voornamenVBinaryList, _sumFrequencyVoornamenV).Naam;
                        achternaam = GeefRandomNaam(_achternamenVBinaryList, _sumFrequencyAchternamenV).Naam;
                        break;
                    default: throw new Exception($"KlantenGenerator.GenereerKlanten: geslacht {geslacht} is onbekend");
                }

                DateTime geboorteDatum = DateTime.Today.AddYears(_minimumleeftijd + Random.Shared.Next(leeftijdDiff)).AddDays(-Random.Shared.Next(365));
                Adress adress = _adressenGenerator.GenereerEenNieuwAdress();

                klanten.Add(new Klant(voornaam, achternaam, adress, geslacht, geboorteDatum));

            }
            return klanten;
        }
    }
}
