using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDatabaseInitialiserDL
{
    public class NaamTextLezer:INamenRepo
    {
        private string _path;

        private Char _seperator;

        private int _naamCol;
        private int? _countCol;
        private NaamSoort _naamSoort;
        private GramaticaalGeslacht _geslacht;

        public NaamTextLezer(string path, char seperator,int naamCol, int? countCol, NaamSoort naamSoort, GramaticaalGeslacht geslacht)
        {
            _path = path;
            _seperator = seperator;
            _naamCol = naamCol;
            _countCol = countCol;
            _geslacht = geslacht;
            _naamSoort = naamSoort;
        }

        public List<NaamInfo> GeefAlleNamen()
        {
            List<NaamInfo> namen = [];
            using (StreamReader sr = new StreamReader(_path)) { 
            string line=sr.ReadLine();

                while (line != null) {
                    try
                    {
                        string[] data = line.Split(_seperator);
                        string naam = data[_naamCol];
                        int aantal;
                        if (_countCol == null)
                        {
                            aantal = 1;
                        }
                        else
                        {
                            aantal = int.Parse(data[_countCol.Value]);
                        }

                        NaamInfo naamInfo = new NaamInfo(naam, aantal, _geslacht, _naamSoort);

                        namen.Add(naamInfo);


                        line= sr.ReadLine();
                    }
                    catch (Exception ex) {
                        line= sr.ReadLine();
                    }
                }

            
            }

            return namen;

        }
    }
}
