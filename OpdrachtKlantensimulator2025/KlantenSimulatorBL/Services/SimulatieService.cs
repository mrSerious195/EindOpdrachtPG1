using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Generator;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Services
{
    public class SimulatieService
    {
        private IKlantenSimulatieExporter _klantenSimulatieExporter;
        private SimulatieManager _simulatieManager;
        private SimulatieGenerator _simulatieGenerator;
        private OpdrachtGeverManager _opdrachtGeverManager;
        private LandDataSetManager _landDataSetManager;


        public SimulatieService(IKlantenSimulatieExporter klantenSimulatieExporter,OpdrachtGeverManager opdrachtGeverManager, LandDataSetManager dataSetManager, SimulatieManager simulatieManager)
        {
            _klantenSimulatieExporter = klantenSimulatieExporter;
            _landDataSetManager =dataSetManager;
          
            _simulatieGenerator = new SimulatieGenerator();
            _simulatieManager = simulatieManager;
            _opdrachtGeverManager= opdrachtGeverManager;
            
        }

        public async Task<SimpeleSimulatieInfoDTO> genereerSimulatieAsync(SimulatieSetupDataDTO data)
        {
            LandDataSet landdata = await _landDataSetManager.GetLandDataSetAsync(data.LandDataSetID);
            Simulatie sim = await _simulatieGenerator.generateSimulatieAsync(data, landdata);
            int simId = await _simulatieManager.SaveSimulatie(sim);
            return await _simulatieManager.GeefSimpeleInfoVanSimulatieAsync(simId);
        }


        public async Task<List<SimpeleSimulatieInfoDTO>> GeefSimpeleInfoVanSimulatiesAsync()
        {
            return await _simulatieManager.GeefSimpeleInfoVanSimulatiesAsync();
        }

        public async Task<List<string>> GeefAlleOpdrachtGeversAsync()
        {
            return await _opdrachtGeverManager.GeefAlleOpdrachtGeversAsync();
        }
        public async Task VoegOpdrachtGeverToe(string naam)
        {
           await  _opdrachtGeverManager.VoegOpdrachtGeverToe(naam);
        }


        public async Task<List<SimpeleLandDataSetDTO>> GeefSimpeleDataSetInfoAsync()
        {
            return await _landDataSetManager.GeefSimpeleDataSetInfoAsync();
        }

        public async Task<SimpeleLandDataSetDTO> GeefSimpeleDataSetInfo(int datasetId)
        {
            return await _landDataSetManager.GeefSimpeleDataSetInfo(datasetId);
        }

        public async Task<List<StadDTO>> GeefInfoStedenAsync(int datasetID)
        {
            return await _landDataSetManager.GeefInfoStedenAsync(datasetID);
        }

        public async Task<DetailedSimulatieDTO> GeefConfigInfoVanSimulatieAsync(int simulatie_id)
        {
            return await _simulatieManager.GeefDetailedInfoVanSimulatieAsync(simulatie_id);
        }

        public async Task<List<StadMetAantalKlantenDTO>> GeefStadKlantenDistributieVanSimulatieAsync(int simulatie_id)
        {
            return await _simulatieManager.GeefStadKlantenDistributieVanSimulatieAsync(simulatie_id);
        }

        public async Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieVoornamenVanSimulatie(int simulatie_id, Geslacht geslacht)
        {
            return await _simulatieManager.GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, geslacht);
        }

        public async Task<List<NaamMetAantalKlantedDTO>> GeefKlantenDistributieAchternamenVanSimulatie(int simulatie_id)
        {
            return await _simulatieManager.GeefKlantenDistributieAchternamenVanSimulatie(simulatie_id);
        }

        public async Task<KlantDTO> InfoOudsteKlant(int simulatie_id)
        {
            return await _simulatieManager.InfoOudsteKlant(simulatie_id);
        }

        public async Task<KlantDTO> InfoJongsteKlant(int simulatie_id)
        {
            return await _simulatieManager.InfoJongsteKlant(simulatie_id);
        }

        public async Task ExportSimulatieMetaDataAlsText(int simulatie_id)
        {
            DetailedSimulatieDTO simdata = await GeefConfigInfoVanSimulatieAsync(simulatie_id);
            List<StadMetAantalKlantenDTO> stedenDistributie = await GeefStadKlantenDistributieVanSimulatieAsync(simulatie_id);
            List<NaamMetAantalKlantedDTO> AchterNamenDistributie = await GeefKlantenDistributieAchternamenVanSimulatie(simulatie_id);

            List<NaamMetAantalKlantedDTO> VrouwenNamenDistributie = await GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, Geslacht.Vrouw);
            List<NaamMetAantalKlantedDTO> mannenNamenDistributie = await GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, Geslacht.Man);
            KlantDTO oudsteKlant = await InfoOudsteKlant(simulatie_id);
            KlantDTO jongsteKlant = await InfoJongsteKlant(simulatie_id);
            _klantenSimulatieExporter.ExportMetaDataAsText(simdata, stedenDistributie, VrouwenNamenDistributie, mannenNamenDistributie, AchterNamenDistributie, jongsteKlant, oudsteKlant);
        }


        public async Task ExportSimulatieMetaDataAlsJson(int simulatie_id) {
            DetailedSimulatieDTO simdata = await GeefConfigInfoVanSimulatieAsync(simulatie_id);
            List<StadMetAantalKlantenDTO> stedenDistributie = await GeefStadKlantenDistributieVanSimulatieAsync(simulatie_id);
            List<NaamMetAantalKlantedDTO> AchterNamenDistributie = await GeefKlantenDistributieAchternamenVanSimulatie(simulatie_id);
            List<NaamMetAantalKlantedDTO> VrouwenNamenDistributie = await GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id,Geslacht.Vrouw);
            List<NaamMetAantalKlantedDTO> mannenNamenDistributie = await GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, Geslacht.Man);
            KlantDTO oudsteKlant = await InfoOudsteKlant(simulatie_id);
            KlantDTO jongsteKlant = await InfoJongsteKlant(simulatie_id);
            _klantenSimulatieExporter.ExportMetaDataAsJson(simdata, stedenDistributie, VrouwenNamenDistributie, mannenNamenDistributie, AchterNamenDistributie, jongsteKlant, oudsteKlant);
        }

        public async Task ExportSimulatieMetaDataAlsTextMetKlanten(int simulatie_id, char seperator)
        {
            DetailedSimulatieDTO simdata = await GeefConfigInfoVanSimulatieAsync(simulatie_id);
            List<StadMetAantalKlantenDTO> stedenDistributie = await GeefStadKlantenDistributieVanSimulatieAsync(simulatie_id);
            List<NaamMetAantalKlantedDTO> AchterNamenDistributie = await GeefKlantenDistributieAchternamenVanSimulatie(simulatie_id);

            List<NaamMetAantalKlantedDTO> VrouwenNamenDistributie = await GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, Geslacht.Vrouw);
            List<NaamMetAantalKlantedDTO> mannenNamenDistributie = await GeefKlantenDistributieVoornamenVanSimulatie(simulatie_id, Geslacht.Man);
            KlantDTO oudsteKlant = await InfoOudsteKlant(simulatie_id);
            KlantDTO jongsteKlant = await InfoJongsteKlant(simulatie_id);
            List<Klant> klanten = await _simulatieManager.GeefKlanten(simulatie_id);
            _klantenSimulatieExporter.ExportMetaDataEnKlantenAsText(seperator,klanten, simdata, stedenDistributie, VrouwenNamenDistributie, mannenNamenDistributie, AchterNamenDistributie, jongsteKlant, oudsteKlant);
        }

    }
}
