using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KlantenSimulatorDLDataSetLezers.Lezers
{
    public class WegJsonLezer : IStratenLezer
    {
        private string _path;
        private string _dotpathStreets;
        private string _dotpathcities;

        private string _landNaam { get; set; }

        public WegJsonLezer(string path, string dotpathStreets, string dotpathcities, string landNaam)
        {
            _path = path;
            _dotpathStreets = dotpathStreets;
            _dotpathcities = dotpathcities;
            _landNaam = landNaam;
        }

        public List<Straat> GeefAlleStraten()
        {
            Land land = new(_landNaam);
            Queue<string> straten;
            List<Stad> steden;
            using (StreamReader sr = new StreamReader(_path))
            {
                string json = sr.ReadToEnd();

                using (var Jdoc = JsonDocument.Parse(json))
                {

                    var stedenJson = Jdoc.RootElement;

                    foreach (string property in _dotpathcities.Split('.'))
                    {
                        stedenJson = stedenJson.GetProperty(property);
                    }
                    steden = stedenJson.Deserialize<string[]>().Select(x=>new Stad(land,x)).ToList();


                    var stratenJson = Jdoc.RootElement;

                    foreach (string property in _dotpathStreets.Split('.'))
                    {
                        stratenJson = stratenJson.GetProperty(property);
                    }
                    straten = new Queue<string>(stratenJson.Deserialize<string[]>());
                }

                

            }
            List<Straat> ret=new();
            foreach(Stad s in steden)
            {
                ret.Add(new Straat(s, straten.Dequeue()));
            }

            while (straten.Count>0)
            {
                Stad stad = steden[Random.Shared.Next(steden.Count)];
                ret.Add(new Straat(stad, straten.Dequeue()));
            }
            return ret;
        }
    }
}
