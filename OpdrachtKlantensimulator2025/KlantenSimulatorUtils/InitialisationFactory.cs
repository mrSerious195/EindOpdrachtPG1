using KlantenSimulatorBL.DomeinControlers;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;
using KlantenSimulatorDatabaseInitialiserDL;
using KlantenSimulatorDatabaseInitialiserDL.interfaces;
using KlantenSimulatorDatabaseInitialiserDL.Lezers;
using KlantenSimulatorDLDatabank;
using KlantenSimulatorDLDataSetLezers.Lezers;
using KlantenSimulatorInitialisationUtils.DTO;
using System;
namespace KlantenSimulatorUtils
{
    public class InitialisationFactory
    {
        private string _landnaam;

        private string _wegPad;

        private List<IConstructorNaamLezerDTO> _infoNaamRepos;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="landnaam">Naam van het Land</param>
        /// <param name="straadPad">Het Pad naar het bestand met de StraatenData</param>
        /// <param name="infoNaamRepos">De Info van de repos van de Namen
        /// 3 namen = vrouwelijve Voornamen, manelijke Voornamen, onzijdige achternamen
        /// 4 namen = vrouwelijve Voornamen, manelijke Voornamen, vrouwelijve achternamen. manelijke achternamen
        /// </param>
        /// <exception cref="ArgumentException"></exception>
        public InitialisationFactory(string landnaam, string straadPad, List<IConstructorNaamLezerDTO> infoNaamRepos)
        {
            ArgumentException.ThrowIfNullOrEmpty(landnaam, nameof(landnaam));
            _landnaam = landnaam;
            ArgumentException.ThrowIfNullOrEmpty(straadPad, nameof(straadPad));
            _wegPad = straadPad;
            if (infoNaamRepos != null)
            {
                if (infoNaamRepos.Contains(null))
                {
                    throw new ArgumentException("infoNaamRepos contains null");
                }
                else
                {
                    switch (infoNaamRepos.Count)
                    {
                        case 3:
                            if (!infoNaamRepos.Any(x => x.Geslacht == GramaticaalGeslacht.Onzijdig && x.NaamSoort == NaamSoort.AchterNaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 3 elements needs AchterNaamOnzijdig");
                            }
                            if (!infoNaamRepos.Any(x => x.Geslacht == GramaticaalGeslacht.Man && x.NaamSoort == NaamSoort.Voornaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 3 elements needs VoorNaamMan");
                            }
                            if (!infoNaamRepos.Any(x => x.Geslacht == GramaticaalGeslacht.Vrouw && x.NaamSoort == NaamSoort.Voornaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 3 elements needs VoorNaamVrouw");
                            }
                            _infoNaamRepos = infoNaamRepos.Select(x => (IConstructorNaamLezerDTO)x.Clone()).ToList();

                            break;

                        case 4:
                            if(!infoNaamRepos.Any(x =>x.Geslacht==GramaticaalGeslacht.Man && x.NaamSoort == NaamSoort.AchterNaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 4 elements needs AchterNaamMan");
                            }
                            if (!infoNaamRepos.Any(x => x.Geslacht == GramaticaalGeslacht.Vrouw && x.NaamSoort == NaamSoort.AchterNaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 4 elements needs AchterNaamVrouw");
                            }
                            if (!infoNaamRepos.Any(x => x.Geslacht == GramaticaalGeslacht.Man && x.NaamSoort == NaamSoort.Voornaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 4 elements needs VoorNaamMan");
                            }
                            if (!infoNaamRepos.Any(x => x.Geslacht == GramaticaalGeslacht.Vrouw && x.NaamSoort == NaamSoort.Voornaam))
                            {
                                throw new ArgumentException($"InitialisationFactory.infoNaamRepos with 4 elements needs VoorNaamVrouw");
                            }
                            _infoNaamRepos = infoNaamRepos.Select(x => (IConstructorNaamLezerDTO)x.Clone()).ToList();
                            break;

                        default:  throw new ArgumentException($"{this.GetType().Name} doesn't accept a infoNaamRepos with {infoNaamRepos.Count} elements");
                    }




                }


            }
            else
            {
                throw new ArgumentException("infoNaamRepos is null");
            }
        }

       public Initialisatieservice MaakDomeinControler()
        {
            ILandDataSetLezer dataSetLezer;
            IStratenLezer stratenRepo = new WegTextLezer(_wegPad, _landnaam);
            switch (_infoNaamRepos.Count)
            {
                case 3:
                    {
                        IConstructorNaamLezerDTO InfoVoornamenV = _infoNaamRepos.Where(x=> x.NaamSoort == NaamSoort.Voornaam && x.Geslacht == GramaticaalGeslacht.Vrouw).First();
                        IConstructorNaamLezerDTO InfoVoornamenM = _infoNaamRepos.Where(x => x.NaamSoort == NaamSoort.Voornaam && x.Geslacht == GramaticaalGeslacht.Man).First();
                        IConstructorNaamLezerDTO InfoFamilienamen = _infoNaamRepos.Where(x => x.NaamSoort == NaamSoort.AchterNaam && x.Geslacht == GramaticaalGeslacht.Onzijdig).First();
                        dataSetLezer = new DatasetLezerAchterNamenZonderGender(stratenRepo, DTOToNamenLezer(InfoVoornamenV), DTOToNamenLezer(InfoVoornamenM), DTOToNamenLezer(InfoFamilienamen));
                        break;
                    }
                case 4:
                    {
                        IConstructorNaamLezerDTO InfoVoornamenV = _infoNaamRepos.Where(x => x.NaamSoort == NaamSoort.Voornaam && x.Geslacht == GramaticaalGeslacht.Vrouw).First();
                        IConstructorNaamLezerDTO InfoVoornamenM = _infoNaamRepos.Where(x => x.NaamSoort == NaamSoort.Voornaam && x.Geslacht == GramaticaalGeslacht.Man).First();
                        IConstructorNaamLezerDTO InfoFamilienamenM = _infoNaamRepos.Where(x => x.NaamSoort == NaamSoort.AchterNaam && x.Geslacht == GramaticaalGeslacht.Man).First();
                        IConstructorNaamLezerDTO InfoFamilienamenV = _infoNaamRepos.Where(x => x.NaamSoort == NaamSoort.AchterNaam && x.Geslacht == GramaticaalGeslacht.Vrouw).First();

                        dataSetLezer = new DatasetLezerAchterNamenMetGender(stratenRepo, DTOToNamenLezer(InfoVoornamenV), DTOToNamenLezer(InfoVoornamenM), DTOToNamenLezer(InfoFamilienamenV), DTOToNamenLezer(InfoFamilienamenM));
                        break;
                    }
                default: throw new Exception($"InitialisationFactory.MaakDomeinControler: doesn't accept a _infoNaamRepos with {_infoNaamRepos.Count} elements");

            }

            DataSetManager dsm = new DataSetManager(new DataSetDBRepo());

            InLeesDataSetManager ildsm = new InLeesDataSetManager(dataSetLezer);

            return new Initialisatieservice(dsm, ildsm);
        }


        private INamenLezer DTOToNamenLezer(IConstructorNaamLezerDTO DTO)
        {
            if(DTO is ConstructorNaamJsonLezerDTO JsonDTO)
            {
                return new NaamJsonLezer(JsonDTO.Path, JsonDTO.JsonPropertyName, JsonDTO.Geslacht, JsonDTO.NaamSoort);
            }
            else if(DTO is ConstructorNaamTextLezerDTO TextDTO)
            {
                return new NaamTextLezer(TextDTO.Path, TextDTO.Seperator, TextDTO.NaamColom, TextDTO.FrequencyColom, TextDTO.NaamSoort, TextDTO.Geslacht);
            }
            else
            {
                throw new NotImplementedException("InitialisationFactory.DTOToNamenLezer: IConstructorNaamLezerDTO is of a type not implemented");
            }

        }
    }
}
