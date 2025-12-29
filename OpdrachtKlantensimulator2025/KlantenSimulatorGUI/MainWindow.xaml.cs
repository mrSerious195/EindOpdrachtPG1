using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Services;
using KlantenSimulatorSimUtils;
using System.Collections.ObjectModel;
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

        private List<SimpeleSimulatieInfoDTO> _hiddenSimulaties;

       
        public MainWindow()
        {
            const string databankString = @"Data Source=ROWALD\SQLEXPRESS; Initial Catalog = klantenGenerator; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False;";
            InitializeComponent();
            _simService = KlantenSimulatieFactory.geefServise(databankString);
            _zichtbareSimulaties = new(_simService.GeefSimpeleInfoVanSimulaties());
            _hiddenSimulaties = new();
           
            SimpleSimGrid.ItemsSource = _zichtbareSimulaties;
        }

      

        private void SearchSimulaties(object sender, RoutedEventArgs e)
        {
           
           

          
            if (Searchbox.Text!=null && !String.IsNullOrEmpty(Searchbox.Text.Trim()))
            {
                List<SimpeleSimulatieInfoDTO> total = new();
                total.AddRange(_zichtbareSimulaties);
                total.AddRange(_hiddenSimulaties);
                _zichtbareSimulaties.Clear();
                _hiddenSimulaties.Clear();

                Func<SimpeleSimulatieInfoDTO, bool> searchFunction;
                String searchtxt = Searchbox.Text;
                if (DatasetRadioButton.IsChecked == true)
                {
                    searchFunction = (SimpeleSimulatieInfoDTO sim) => { return sim.NaamDataSet.Contains(searchtxt); };
                }
                else if (LandRadioButton.IsChecked == true) {
                    searchFunction = (SimpeleSimulatieInfoDTO sim) => { return sim.Land.Contains(searchtxt); };
                }
                else if (OpdrachtGeverRadioButton.IsChecked==true)
                {
                    searchFunction = (SimpeleSimulatieInfoDTO sim) => { return sim.OpdrachtGever.Contains(searchtxt); };
                }
                else
                {
                    throw new NotImplementedException("search with that searchoption not implemented");
                }
               
                foreach(var sim in total)
                {
                    if (searchFunction(sim)) {
                        _zichtbareSimulaties.Add(sim);
                    }
                    else
                    {
                        _hiddenSimulaties.Add(sim);
                    }
                }
              
            }
            else
            {
                
                foreach (var sim in  _hiddenSimulaties)
                {
                    _zichtbareSimulaties.Add(sim);
                }
                _hiddenSimulaties.Clear();
            }
        


        }


       
    }
}