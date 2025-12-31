using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NieuweKlantenSimulatieWindow.xaml
    /// </summary>
    public partial class NieuweKlantenSimulatieWindow : Window
    {
        private SimulatieService _simService;
        private ObservableCollection<string> _alleopdrachtgevers;
        public NieuweKlantenSimulatieWindow(SimulatieService simulatieService)
        {
            InitializeComponent();
            _simService=simulatieService;
            _alleopdrachtgevers = new(_simService.GeefAlleOpdrachtGevers());
            OpdrachtgeverBox.ItemsSource = _alleopdrachtgevers;
            OpdrachtgeverBox.SelectedIndex = 0;
            DatasetBox.ItemsSource = _simService.GeefSimpeleDataSetInfo();
            DatasetBox.SelectedIndex = 0;
        }

        public void NieuweOpdrachtGever(object sender, RoutedEventArgs e)
        {
            var window = new NewOpdrachtgeverWindow(_simService, _alleopdrachtgevers);
            window.Show();
        }
        public void KansOpLetterChanged(object sender, RoutedPropertyChangedEventArgs<double> newValue)
        {
            if (HuisNummerKansLabel != null)
            {
                float nieuweKans = MathF.Round((float)newValue.NewValue, 2);
                HuisNummerKansLabel.Content = $"{nieuweKans}";
            }
        }

        

            public void MogelijkeLetterChanged(object sender, RoutedPropertyChangedEventArgs<double> newValue)
        {
            if (LetterLabel != null)
            {

                int NewMaxLetter = (int)newValue.NewValue;
                if (NewMaxLetter == 1)
                {
                    LetterLabel.Content = "A";
                }
                else
                {
                    LetterLabel.Content = $"A - {(char)((int)'A' + NewMaxLetter - 1)}";
                }
            }
        }

        public void GenerateNewKlantenset(object sender, RoutedEventArgs e)
        {
            int seed = int.Parse(SeedBox.Text);
            int aantalKlanten = int.Parse(AantalKlantenBox.Text);
            int minimumLeeftijd = int.Parse(MinimumLeeftijdBox.Text);
            int maximumLeeftijd = int.Parse(MaximumLeeftijdBox.Text);
            int MaximumAantalHuisNummer = int.Parse(MaxHuisNrBox.Text);
            float kansVoorHuisNrMetLetter = (float)KansOpLetterSlider.Value;
            int aantalMogelijkeLettersInHuisNr = (int)LetterInHuisNrSlider.Value;
            int maximumHuisNr = int.Parse(MaxHuisNrBox.Text);
            int landDatasetId = ((SimpeleLandDataSetDTO)DatasetBox.SelectedValue).DataSetID;
            List<int> gelimiteerdTotStedenStadIDs = new();
            string opdrachtgever = (string)OpdrachtgeverBox.SelectedValue;

            SimulatieSetupDataDTO data = new(seed, landDatasetId,minimumLeeftijd,maximumLeeftijd,maximumHuisNr,aantalMogelijkeLettersInHuisNr, aantalKlanten,kansVoorHuisNrMetLetter,opdrachtgever,gelimiteerdTotStedenStadIDs);
            _simService.genereerSimulatie(data);
        }
    }
}
