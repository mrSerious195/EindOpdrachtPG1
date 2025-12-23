using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDatabaseInitialiserDL.Lezers
{
    public class DatasetLezerAchterNamenMetGender : ILandDataSetLezer
    {
        public DatasetLezerAchterNamenMetGender(string naamDataSet,IStratenLezer stratenRepo, INamenLezer namenRepoV, INamenLezer namenRepoM, INamenLezer namenRepoAchterNaamV, INamenLezer namenRepoAchterNaamM)
        {
            _stratenRepo = stratenRepo;
            _namenRepoV = namenRepoV;
            _namenRepoM = namenRepoM;
            _namenRepoAchterNaamV = namenRepoAchterNaamV;
            _namenRepoAchterNaamM = namenRepoAchterNaamM;
            _naamDataset = naamDataSet;
        }

        private IStratenLezer _stratenRepo { get; init; }

        private string _naamDataset;
        private INamenLezer _namenRepoV { get; init; }
        private INamenLezer _namenRepoM { get; init; }
        private INamenLezer _namenRepoAchterNaamV { get; init; }
        private INamenLezer _namenRepoAchterNaamM { get; init; }

        public LandDataSet LeesLandDataSet()
        {
            List<Straat> wegen = _stratenRepo.GeefAlleStraten();
            List<NaamInfo> voornamenV = _namenRepoV.GeefAlleNamen();
            List<NaamInfo> voornamenM = _namenRepoM.GeefAlleNamen();
            List<NaamInfo> achternamenV = _namenRepoAchterNaamV.GeefAlleNamen();
            List<NaamInfo> achternamenM = _namenRepoAchterNaamM.GeefAlleNamen();
            Land land = wegen.First().Stad.Land;
            return new LandDataSet(false,_naamDataset,land, voornamenM, voornamenV, achternamenM, achternamenV, wegen);
        }
    }
}
