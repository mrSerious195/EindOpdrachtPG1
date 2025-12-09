using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DomeinControlers
{
    public class InitialisatieDC
    {
        private DataSetManager _dataSetManager;
        private ILandDataSetLezer _dataSetLezer;

        public InitialisatieDC(DataSetManager dataSetManager, ILandDataSetLezer dataSetLezer)
        {
            _dataSetManager = dataSetManager;
            _dataSetLezer = dataSetLezer;
        }


        public LandDataSet GetDataSet() {
            return _dataSetLezer.LeesLandDataSet();
        }

       
    }
}
