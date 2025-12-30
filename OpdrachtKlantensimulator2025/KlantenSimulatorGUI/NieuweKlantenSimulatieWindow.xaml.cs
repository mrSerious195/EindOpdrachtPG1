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
        }

        public void NieuweOpdrachtGever(object sender, RoutedEventArgs e)
        {
            var window = new NewOpdrachtgeverWindow(_simService, _alleopdrachtgevers);
            window.Show();
        }
    }
}
