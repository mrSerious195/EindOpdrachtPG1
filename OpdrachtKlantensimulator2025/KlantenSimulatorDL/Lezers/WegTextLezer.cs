using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDatabaseInitialiserDL.Lezers
{
    public class WegTextLezer: IStratenLezer
    {
        private string _path {  get; set; }

        private string _landNaam { get; set; }



        public WegTextLezer(string path,string landNaam)
        {
            _path = path;
            _landNaam = landNaam;
        }

        private string[] _validHighwayTypes = ["residential", "primary", "secondary", "pedestrian", "tertiary", "unclassified", "living_street"];

        public List<Straat> GeefAlleStraten()
        {
        const string unknownStad = "(unknown)";
        const string HighwayType = "HighwayType"; // primary, secondary, thirtary, unclasified, residential, living street 
        


            HashSet<Straat> straten = [];

            Dictionary<string, Stad> stadnaamNaarStad = new();
            Land land = new(_landNaam);
            using (StreamReader sr = new(_path))
            {
                string line = sr.ReadLine();

                while (line != null)
                {
                    try
                    {
                        string[] info = line.Split(';');
                        string StadNaam = info[0];
                        string StraatNaam = info[1];
                        string Staatsoort = info[2];
                        if (StadNaam != unknownStad && _validHighwayTypes.Contains(Staatsoort))
                        {
                            Stad stad;
                            if (stadnaamNaarStad.TryGetValue(StadNaam, out stad))
                            {

                            }
                            else
                            {
                                stad = new(land, StadNaam);
                                stadnaamNaarStad[StadNaam] = stad;
                            }

                           
                            Straat straat = new(stad, StraatNaam);
                            if (!straten.Contains(straat))
                            {
                                straten.Add(straat);
                            }

                        }


                        line = sr.ReadLine();
                    }
                    catch (Exception e)
                    {
                        line = sr.ReadLine();
                    }
                }
            }
            

            return straten.ToList();
        }
    }
}
