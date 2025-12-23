using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDatabaseInitialiserDL.Lezers
{
    public class DatasetLezerAchterNamenZonderGender: ILandDataSetLezer
    {
        public DatasetLezerAchterNamenZonderGender(string naamDataset,IStratenLezer stratenRepo, INamenLezer namenRepoV, INamenLezer namenRepoM, INamenLezer namenRepoAchterNaam)
        {
            _stratenRepo = stratenRepo;
            _namenRepoV = namenRepoV;
            _namenRepoM = namenRepoM;
            _namenRepoAchterNaam = namenRepoAchterNaam;
            _naamDataset = naamDataset;
        }
        private string _naamDataset;
        private IStratenLezer _stratenRepo { get; init; }

        private INamenLezer _namenRepoV { get; init; }
        private INamenLezer _namenRepoM { get; init; }
        private INamenLezer _namenRepoAchterNaam { get; init; }

        public LandDataSet LeesLandDataSet()
        {
            List<Straat> wegen = _stratenRepo.GeefAlleStraten();
            List<NaamInfo> voornamenV = _namenRepoV.GeefAlleNamen();
            List<NaamInfo> voornamenM = _namenRepoM.GeefAlleNamen();
            List<NaamInfo> achternamenV = _namenRepoAchterNaam.GeefAlleNamen();
            List<NaamInfo> achternamenM = achternamenV;
            Land land = wegen.First().Stad.Land;
            return new LandDataSet(true,_naamDataset,land, voornamenM, voornamenV, achternamenM, achternamenV, wegen);
        }
    }
}
