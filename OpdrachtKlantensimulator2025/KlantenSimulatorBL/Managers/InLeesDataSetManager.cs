using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Managers
{
    public class InLeesDataSetManager
    {
        private ILandDataSetLezer _dataSetLezer;

        public LandDataSet? LandDataSet { get; private set; }

        public InLeesDataSetManager(ILandDataSetLezer dataSetLezer)
        {
            _dataSetLezer = dataSetLezer;
        }

        public void LeesDataSet()
        {
            LandDataSet = _dataSetLezer.LeesLandDataSet();
        }


      
    }
}
