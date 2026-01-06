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
    public class LandDataSetManager
    {
        private ILandDataSetRepo _repo;

        public LandDataSetManager(ILandDataSetRepo repo)
        {
            _repo = repo;
        }

        public void SaveLandDataSet(LandDataSet landData)
        {
            _repo.SaveLandDataSet(landData);
        }

        public async Task<LandDataSet> GetLandDataSetAsync(int DatasetId)
        {
            return await _repo.GetLandDataSet(DatasetId);
        }

        public async Task<List<SimpeleLandDataSetDTO>> GeefSimpeleDataSetInfoAsync()
        {
            return await _repo.GeefSimpeleDataSetInfo();
        }

        public async Task<List<StadDTO>> GeefInfoStedenAsync(int datasetID)
        {
            return await _repo.GeefInfoSteden(datasetID);
        }

        public async Task<SimpeleLandDataSetDTO> GeefSimpeleDataSetInfo(int datasetId)
        {
            return await _repo.GeefSimpeleDataSetInfo( datasetId);
        }
    }
}
