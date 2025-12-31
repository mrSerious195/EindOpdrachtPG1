using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface ILandDataSetRepo
    {
        public void SaveLandDataSet(LandDataSet landData);


        public LandDataSet GetLandDataSet(int datasetId);

        public List<SimpeleLandDataSetDTO> GeefSimpeleDataSetInfo();
    }
}