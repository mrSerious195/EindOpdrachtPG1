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


        public Task<LandDataSet> GetLandDataSet(int datasetId);

        public Task<List<SimpeleLandDataSetDTO>> GeefSimpeleDataSetInfo();

        public Task<List<StadDTO>> GeefInfoSteden(int datasetID);

        public Task<SimpeleLandDataSetDTO> GeefSimpeleDataSetInfo(int datasetId);
    }
}