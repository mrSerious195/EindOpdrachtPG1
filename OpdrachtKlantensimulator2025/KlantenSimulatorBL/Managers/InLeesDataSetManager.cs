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


        public LandDataSetDTO geefDataSetAlsDTO()
        {
            if (LandDataSet == null)
            {
                throw new Exception("InLeesDataSetManager heeft nog geen data gelezen");
            }
            return new LandDataSetDTO()
            {
                Land = LandDataSet.Straten[0].Stad.Land.Naam,
                VoornamenV = LandDataSet.VoornamenV.Select(v => v.Naam).ToArray(),
                VoornamenM = LandDataSet.VoornamenM.Select(m => m.Naam).ToArray(),
                AxchternamenV = LandDataSet.AchternamenV.Select(v => v.Naam).ToArray(),
                AxchternamenM = LandDataSet.AchternamenM.Select(m => m.Naam).ToArray(),
                StadDTOs = LandDataSet.Straten
                .GroupBy(straat => straat.Stad)
                .Select(group => new StadDTO(group.Key.Naam, group.Key.Id, group.Select(straat => straat.Naam).ToArray())).ToArray()
            };
        }
    }
}
