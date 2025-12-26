using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Generator
{
    public class AdressenGenerator : IAdressenGenerator
    {
        private List<Straat> _straten;
        private Dictionary<(Straat, int), bool> IsHuisEenAppartement;
        private int _maxHuisnummer;
        private bool _zijnErLetterInHuisNr;
        private float _kansOpEenLetterInHuisNr;
        private int _aantalMogelijkeLettersInHuisNr;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kansOpEenLetterInHuisNr"> 0 betekend geen letters in de huisNrs, 1 betekent dat er overal een letter in een huisnr zit</param>
        /// <param name="maxHuisnummer"></param>
        ///  <param name="aantalMogelijkeLettersInHuisNr">1 = A | 2 = A,B | 3 =A,B,C ...</param>
        public AdressenGenerator(List<Straat> straten, float kansOpEenLetterInHuisNr, int maxHuisnummer, int aantalMogelijkeLettersInHuisNr)
        {

            _straten = straten;
            _maxHuisnummer = maxHuisnummer;
            if (kansOpEenLetterInHuisNr > 0)
            {
                _zijnErLetterInHuisNr = true;
            }
            else
            {
                _zijnErLetterInHuisNr = false;
            }
            _kansOpEenLetterInHuisNr = kansOpEenLetterInHuisNr;
            IsHuisEenAppartement = new();
            _aantalMogelijkeLettersInHuisNr = aantalMogelijkeLettersInHuisNr;



        }

        public Adress GenereerEenNieuwAdress()
        {

           
            Straat straat = _straten[Random.Shared.Next(_straten.Count)];

            int huisNrAlsInt = Random.Shared.Next(_maxHuisnummer) + 1;
            Adress adress;
            if (_zijnErLetterInHuisNr)
            {
                if (IsHuisEenAppartement.TryGetValue((straat, huisNrAlsInt), out bool isEenApartement))
                {

                }
                else
                {
                    isEenApartement = Random.Shared.NextDouble() < _kansOpEenLetterInHuisNr;
                    IsHuisEenAppartement.Add((straat, huisNrAlsInt), isEenApartement);
                }

                if (isEenApartement)
                {
                    char appartementChar = (char)('A' + Random.Shared.Next(_aantalMogelijkeLettersInHuisNr));
                    string huisnr = $"{huisNrAlsInt}{appartementChar}";
                    adress = new(huisnr, straat);
                }
                else
                {
                    adress = new($"{huisNrAlsInt}", straat);
                }
            }
            else
            {
                adress = new($"{huisNrAlsInt}", straat);
            }


            return adress;
        }
    }
}

