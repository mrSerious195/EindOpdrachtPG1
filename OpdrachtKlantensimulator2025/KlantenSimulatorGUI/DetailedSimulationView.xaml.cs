using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KlantenSimulatorGUI
{
    /// <summary>
    /// Interaction logic for DetailedSimulationView.xaml
    /// </summary>
    public partial class DetailedSimulationView : Window
    {
        private SimulatieService _simService;
        private int _landDatasetId;
        public DetailedSimulationView(SimulatieService simservise,int simulatieId)
        {
            InitializeComponent();

            _simService=simservise;
        
            SetData(simservise, simulatieId);
                }

        private async void SetData(SimulatieService simservise, int simulatieId)
        {
            // StadKlandGrid populeren
            List<StadMetAantalKlantenDTO> KlantenInsteden = await simservise.GeefStadKlantenDistributieVanSimulatieAsync(simulatieId);
            CollectionViewSource klantenInStedenView = new CollectionViewSource();
            klantenInStedenView.Source = KlantenInsteden;
            klantenInStedenView.SortDescriptions.Add(
                new SortDescription(nameof(StadMetAantalKlantenDTO.aantalKlanten), ListSortDirection.Descending));

            StadKlandGrid.ItemsSource = klantenInStedenView.View;


            // vrouwengrid populeren
            List<NaamMetAantalKlantedDTO> VrouwenNamen = await simservise.GeefKlantenDistributieVoornamenVanSimulatie(simulatieId,Geslacht.Vrouw);
            CollectionViewSource VrouwenMetNamenView = new CollectionViewSource();
            VrouwenMetNamenView.Source = VrouwenNamen;
            VrouwenMetNamenView.SortDescriptions.Add(
                new SortDescription(nameof(NaamMetAantalKlantedDTO.AantalKlantenMetNaam), ListSortDirection.Descending));
            VrouwenNamenGrid.ItemsSource = VrouwenMetNamenView.View;

            // mannangrid populeren
            List<NaamMetAantalKlantedDTO> MannenNamen = await simservise.GeefKlantenDistributieVoornamenVanSimulatie(simulatieId, Geslacht.Man);
            CollectionViewSource MannenMetNamenView = new CollectionViewSource();
            MannenMetNamenView.Source = MannenNamen;
            MannenMetNamenView.SortDescriptions.Add(
                new SortDescription(nameof(NaamMetAantalKlantedDTO.AantalKlantenMetNaam), ListSortDirection.Descending));
            ManneNammenGrid.ItemsSource = MannenMetNamenView.View;

            // achternamengrid populeren
            List<NaamMetAantalKlantedDTO> AchterNamen = await simservise.GeefKlantenDistributieAchternamenVanSimulatie(simulatieId);
            CollectionViewSource AchterNamenView = new CollectionViewSource();
            AchterNamenView.Source = VrouwenNamen;
            AchterNamenView.SortDescriptions.Add(
                new SortDescription(nameof(NaamMetAantalKlantedDTO.AantalKlantenMetNaam), ListSortDirection.Descending));
            AchterNamenGrid.ItemsSource = AchterNamenView.View;




            // opvragen van info voor SimulatieGegevensBlock en ExtraGegevensBlock
            DetailedSimulatieDTO simdata = await simservise.GeefConfigInfoVanSimulatieAsync(simulatieId);
            _landDatasetId = simdata.DatasetId;
            KlantDTO jongsteKlant = await simservise.InfoJongsteKlant(simulatieId);
            KlantDTO oudsteKlant = await simservise.InfoOudsteKlant(simulatieId);


            // de complexere tekst voor SimulatieGegevensBlock genereren
            string mogelijkeLetters = simdata.AantalMogelijkeLettersInHuisNummer == 1 ? "[A]" : $"[A - {(char)('A' - 1 + simdata.AantalMogelijkeLettersInHuisNummer)}]";
            string stadlimiet = simdata.GelimiteerdTotSteden.Count() == 0 ? "Alle steden in de dataset worden gebruikt"
                : $"De simulatie is gelimiteerd tot de volgende steden {String.Join(", ", simdata.GelimiteerdTotSteden)}";

            // tekst in  SimulatieGegevensBlock plaatsen

            SimulatieGegevensBlock.Inlines.Add(new Run($"Opdrachtgever: {simdata.OpdrachtGever}\n"));
            SimulatieGegevensBlock.Inlines.Add(new Run($"Dataset: {simdata.NaamDataSet}     Land: {simdata.Land} \n"));
            SimulatieGegevensBlock.Inlines.Add(new Run($"Aantal klanten: {simdata.SimulatieGrote}\n"));
            SimulatieGegevensBlock.Inlines.Add(new Run($"Minimum leeftijd: {simdata.MinimumleeftijdSettings}       Maximum leeftijd: {simdata.MaximumleeftijdSettings}\n"));
            SimulatieGegevensBlock.Inlines.Add(new Run($"Kans op een letter in het huisnummer: {simdata.KansOpLettersInHuisNummer}       Mogelijke Letters: {mogelijkeLetters}\n"));
            SimulatieGegevensBlock.Inlines.Add(new Run(stadlimiet));


            // tekst in  SimulatieGegevensBlock plaatsen

            ExtraInfoBlock.Inlines.Add(new Run($"Gemidelde leeftijd Op aanmaak {simdata.GemideldeSimulatieLeefrijdOpMomentVanAanMaak} jaar \n"));
            ExtraInfoBlock.Inlines.Add(new Run($"Gemidelde leeftijd Op nu {simdata.GemideldeSimulatieLeefrijdOpMomentVanOpvraagGegenens} jaar\n"));
            ExtraInfoBlock.Inlines.Add(new Run($"De jongste klant is: {jongsteKlant.Voornaam} {jongsteKlant.AchterNaam} geboren op {jongsteKlant.GeboorteDatum.ToLongDateString()} en wondende {jongsteKlant.Straat} {jongsteKlant.HuisNr}, {jongsteKlant.Stad} ({jongsteKlant.Land})\n"));
            ExtraInfoBlock.Inlines.Add(new Run($"De Oudste klant is: {oudsteKlant.Voornaam} {oudsteKlant.AchterNaam} geboren op {oudsteKlant.GeboorteDatum.ToLongDateString()} en wondende {oudsteKlant.Straat} {oudsteKlant.HuisNr}, {oudsteKlant.Stad} ({oudsteKlant.Land})"));


        }


        public void ExtraInfoDataset(object sender, RoutedEventArgs e)
        {
            var window = new LandDatasetInfo(_simService, _landDatasetId);
            window.Show();
        }
    }
}
