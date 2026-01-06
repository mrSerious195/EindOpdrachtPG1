using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
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
    public class Initialisatieservice
    {
        private LandDataSetManager _dataSetManager;
        private InLeesDataSetManager _inLeesDataSetManager;

        public Initialisatieservice(LandDataSetManager dataSetManager, InLeesDataSetManager inLeesDataSetManager)
        {
            _dataSetManager = dataSetManager;
            _inLeesDataSetManager = inLeesDataSetManager;
            _inLeesDataSetManager.LeesDataSet();
        }

       

       

        public void SaveDataSet()
        {
            _dataSetManager.SaveLandDataSet(_inLeesDataSetManager.LandDataSet);
        }
    }
}
