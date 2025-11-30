using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDatabaseInitialiserDL
{
    public class NaamTextLezer:INamenRepo
    {
        private string _path;

        private string _countryName;

        public NaamTextLezer(string path, string countryName)
        {
            _path = path;
            _countryName = countryName;
        }

        public List<NaamInfo> GeefAlleNamen()
        {
            List<NaamInfo> namen = [];
            using (StreamReader sr = new StreamReader(_path)) { 
            string line=sr.ReadLine();

                while (line != null) {
                    string data = line.Split(';');
                
                sr.ReadLine();
                }

            
            }

            return namen;

        }
    }
}
