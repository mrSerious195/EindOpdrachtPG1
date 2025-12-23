using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Managers
{
    public class DataSetManager
    {
        private ILandDataSetRepo _repo;

        public DataSetManager(ILandDataSetRepo repo)
        {
            _repo = repo;
        }

        public void SaveLandDataSet(LandDataSet landData)
        {
            _repo.SaveLandDataSet(landData);
        }

        public LandDataSet GetLandDataSet(int DatasetId)
        {
            return _repo.GetLandDataSet(DatasetId);
        }

    }
}
