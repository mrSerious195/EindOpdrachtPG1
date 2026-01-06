using KlantenSimulatorBL.DomeinControlers;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;
using KlantenSimulatorDatabaseInitialiserDL;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using KlantenSimulatorDatabaseInitialiserDL.Lezers;
using KlantenSimulatorDLDatabank;
using KlantenSimulatorDLDataSetLezers.Lezers;
using KlantenSimulatorInitialisationUtils.DTO.ConstructorStraatJsonLezerDTO;
using KlantenSimulatorInitialisationUtils.DTO.NaamConstructerInfo;
using KlantenSimulatorInitialisationUtils.DTO.StadConstructorInfo;
using System;
namespace KlantenSimulatorUtils
{
    public class InitialisationFactory
    {
        private string _naamDataset;
        private string _landnaam;

        private IConstructorStreetLezerDTO _wegPad;

        private IConstructorNaamLezerDTO _voornamenV;
        private IConstructorNaamLezerDTO _voornamenM;
        private IConstructorNaamLezerDTO _achternamenV;
        private IConstructorNaamLezerDTO _achternamenM;

        private bool _zijnAchternamenNeutraal;

        private string _databankConnectionString;

       


        public InitialisationFactory(string naamDataset , string landnaam, IConstructorStreetLezerDTO straadPad, IConstructorNaamLezerDTO voornamenV
, IConstructorNaamLezerDTO voornamenM, IConstructorNaamLezerDTO achternamenV, IConstructorNaamLezerDTO achternamenM, string databankConnectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(landnaam, nameof(landnaam));
            _landnaam = landnaam;
            
            ArgumentException.ThrowIfNullOrEmpty(databankConnectionString, nameof(databankConnectionString));
            _databankConnectionString = databankConnectionString;
            _wegPad = straadPad;
            ArgumentException.ThrowIfNullOrEmpty(naamDataset, nameof(naamDataset));
            _naamDataset=naamDataset;
            _voornamenV = (IConstructorNaamLezerDTO)voornamenV.Clone();
            _voornamenM = (IConstructorNaamLezerDTO)voornamenM.Clone();
            _achternamenM = (IConstructorNaamLezerDTO)achternamenM.Clone();
            _achternamenV = (IConstructorNaamLezerDTO)achternamenV.Clone();
            _zijnAchternamenNeutraal = false;
        }

        public InitialisationFactory(string naamDataset, string landnaam, IConstructorStreetLezerDTO straadPad, IConstructorNaamLezerDTO voornamenV
, IConstructorNaamLezerDTO voornamenM, IConstructorNaamLezerDTO achternamen, string databankConnectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(landnaam, nameof(landnaam));
            _landnaam = landnaam;

            ArgumentException.ThrowIfNullOrEmpty(databankConnectionString, nameof(databankConnectionString));
            _databankConnectionString = databankConnectionString;
          
            _wegPad = straadPad;
            ArgumentException.ThrowIfNullOrEmpty(naamDataset, nameof(naamDataset));
            _naamDataset = naamDataset;
            _voornamenV = (IConstructorNaamLezerDTO)voornamenV.Clone();
            _voornamenM = (IConstructorNaamLezerDTO)voornamenM.Clone();
            _achternamenM = (IConstructorNaamLezerDTO)achternamen.Clone();
            _achternamenV = (IConstructorNaamLezerDTO)achternamen.Clone();
            _zijnAchternamenNeutraal=true;
        }

        public Initialisatieservice MaakDomeinControler()
        {
            IStratenLezer stratenRepo = GeefWeglezer(_wegPad);
            ILandDataSetLezer dataSetLezer;
            if (_zijnAchternamenNeutraal)
            {
                 dataSetLezer = new DatasetLezerAchterNamenZonderGender(_naamDataset, stratenRepo,
                     DTOToNamenLezer(_voornamenV, GramaticaalGeslacht.Vrouw, NaamSoort.Voornaam),
                     DTOToNamenLezer(_voornamenM, GramaticaalGeslacht.Man, NaamSoort.Voornaam),
                     DTOToNamenLezer(_achternamenV, GramaticaalGeslacht.Onzijdig, NaamSoort.AchterNaam));
            }
            else
            {
                 dataSetLezer = new DatasetLezerAchterNamenMetGender(_naamDataset, stratenRepo,
                     DTOToNamenLezer(_voornamenV,GramaticaalGeslacht.Vrouw, NaamSoort.Voornaam),
                     DTOToNamenLezer(_voornamenM, GramaticaalGeslacht.Man, NaamSoort.Voornaam),
                     DTOToNamenLezer(_achternamenV, GramaticaalGeslacht.Vrouw, NaamSoort.AchterNaam),
                     DTOToNamenLezer(_achternamenM, GramaticaalGeslacht.Man, NaamSoort.AchterNaam));

            }


            LandDataSetManager dsm = new LandDataSetManager(new LandDataSetDBRepo(_databankConnectionString));

            InLeesDataSetManager ildsm = new InLeesDataSetManager(dataSetLezer);

            return new Initialisatieservice(dsm, ildsm);
        }


        private INamenLezer DTOToNamenLezer(IConstructorNaamLezerDTO DTO,GramaticaalGeslacht Geslacht, NaamSoort NaamSoort)
        {
            if(DTO is ConstructorNaamJsonLezerDTO JsonDTO)
            {
                return new NaamJsonLezer(JsonDTO.Path, JsonDTO.JsonPropertyName, Geslacht, NaamSoort);
            }
            else if(DTO is ConstructorNaamTextLezerDTO TextDTO)
            {
                return new NaamTextLezer(TextDTO.Path, TextDTO.Seperator, TextDTO.NaamColom, TextDTO.FrequencyColom, NaamSoort, Geslacht);
            }
            else
            {
                throw new NotImplementedException("InitialisationFactory.DTOToNamenLezer: IConstructorNaamLezerDTO is of a type not implemented");
            }

        }


        private IStratenLezer GeefWeglezer(IConstructorStreetLezerDTO DTO)
        {
            if (DTO is ConstructorStraatJsonLezerDTO JsonDTO)
            {
                return new WegJsonLezer(JsonDTO.Path, JsonDTO.DotPathStraten, JsonDTO.DotPathSteden, _landnaam);
            }
            else if (DTO is ConstructorStraatTXTLezerDTO TextDTO)
            {
                return new WegTextLezer(TextDTO.Path, _landnaam);
            }
            else
            {
                throw new NotImplementedException("InitialisationFactory.DTOToNamenLezer: IConstructorNaamLezerDTO is of a type not implemented");
            }

        }
    }
}
