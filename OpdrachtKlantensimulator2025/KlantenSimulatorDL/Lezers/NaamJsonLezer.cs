using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KlantenSimulatorDLDataSetLezers.Lezers
{
    public class NaamJsonLezer : INamenLezer
    {

        private string _path = @"C:\Users\rowal\Downloads\SourceData\SourceData\Polen\pl.locale.json";
        private string _propertyName = "name.first_name_male";
        private GramaticaalGeslacht _geslacht;
        private NaamSoort naamSoort;


        public NaamJsonLezer(string path, string propertyName, GramaticaalGeslacht geslacht, NaamSoort naamSoort)
        {
            _path = path;
            _propertyName = propertyName;
            _geslacht = geslacht;
            this.naamSoort = naamSoort;
        }

        public List<NaamInfo> GeefAlleNamen()
        {
            string[] namen;
            using (StreamReader sr = new StreamReader(_path))
            {
                string json = sr.ReadToEnd();
             
                using (var Jdoc = JsonDocument.Parse(json))
                {

                    var NamenJson = Jdoc.RootElement;

                    foreach (string property in _propertyName.Split('.'))
                    {
                        NamenJson = NamenJson.GetProperty(property);
                    }
                     namen = NamenJson.Deserialize<string[]>();
                   
                }
               
            }
            return namen.Select(n => new NaamInfo(n, 1, _geslacht, naamSoort)).ToList();
        }
    }
}