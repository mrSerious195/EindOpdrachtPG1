using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Services;
using KlantenSimulatorSimUtils;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KlantenSimulatorGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SimulatieService _simService;
        private ObservableCollection<SimpeleSimulatieInfoDTO> _zichtbareSimulaties;
        private ObservableCollection<SimpeleSimulatieInfoDTO> _alleSimulaties;
        

       
        public MainWindow()
        {
            const string databankString = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False;";
            InitializeComponent();
            _simService = KlantenSimulatieFactory.GeefServise(databankString);
            PopulateAsync();
        }

        private async void PopulateAsync()
        {
            List < SimpeleSimulatieInfoDTO > sims = await _simService.GeefSimpeleInfoVanSimulatiesAsync();
            _zichtbareSimulaties = new(sims);
            _alleSimulaties = new(sims);
            _alleSimulaties.CollectionChanged += (object o, NotifyCollectionChangedEventArgs h) => Search();
            SimpleSimGrid.ItemsSource = _zichtbareSimulaties;
        }

      

        private void SearchSimulaties(object sender, RoutedEventArgs e)
        {

            Search();


        }


        private void Search()
        {
            if (Searchbox.Text != null && !String.IsNullOrEmpty(Searchbox.Text.Trim()))
            {

                _zichtbareSimulaties.Clear();


                Func<SimpeleSimulatieInfoDTO, bool> searchFunction;
                String searchtxt = Searchbox.Text;
                if (DatasetRadioButton.IsChecked == true)
                {
                    searchFunction = (SimpeleSimulatieInfoDTO sim) => { return sim.NaamDataSet.Contains(searchtxt); };
                }
                else if (LandRadioButton.IsChecked == true)
                {
                    searchFunction = (SimpeleSimulatieInfoDTO sim) => { return sim.Land.Contains(searchtxt); };
                }
                else if (OpdrachtGeverRadioButton.IsChecked == true)
                {
                    searchFunction = (SimpeleSimulatieInfoDTO sim) => { return sim.OpdrachtGever.Contains(searchtxt); };
                }
                else
                {
                    throw new NotImplementedException("search with that searchoption not implemented");
                }

                foreach (var sim in _alleSimulaties)
                {
                    if (searchFunction(sim))
                    {
                        _zichtbareSimulaties.Add(sim);
                    }

                }

            }
            else
            {
                _zichtbareSimulaties.Clear();
                foreach (var sim in _alleSimulaties)
                {
                    _zichtbareSimulaties.Add(sim);
                }

            }
        }


        private void NieuseSimulatie(object sender, RoutedEventArgs e)
        {
           var window = new NieuweKlantenSimulatieWindow(_simService, _alleSimulaties);
            window.Show();
        }

        public void MoreInfoClicked(object sender, RoutedEventArgs e)
        {
            var selectedRow = SimpleSimGrid.SelectedItem as SimpeleSimulatieInfoDTO;
            if (selectedRow != null)
            {
                var window = new DetailedSimulationView(_simService, selectedRow.Id);
                window.Show();

            }
        }

        public async void ExportMetadataAsText(object sender, RoutedEventArgs e)
        {
            var selectedRow = SimpleSimGrid.SelectedItem as SimpeleSimulatieInfoDTO;
            if (selectedRow != null)
            {
               await _simService.ExportSimulatieMetaDataAlsText(selectedRow.Id);
                MessageBox.Show("Finished exporting");
            }
        }


        public async void ExportMetaDataAsJson(object sender, RoutedEventArgs e)
        {
            var selectedRow = SimpleSimGrid.SelectedItem as SimpeleSimulatieInfoDTO;
            if (selectedRow != null)
            {
                await _simService.ExportSimulatieMetaDataAlsJson(selectedRow.Id);
                MessageBox.Show("Finished exporting");
            }
        }


        public void ExportSimulatieMetaDataAlsTextMetKlanten(object sender, RoutedEventArgs e)
        {
            var selectedRow = SimpleSimGrid.SelectedItem as SimpeleSimulatieInfoDTO;
            if (selectedRow != null)
            {
               var window = new ExportWithKlantenWindow( _simService,selectedRow.Id);
                window.Show();
            }
        }



    }
}