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
    /// Interaction logic for NewOpdrachtgeverWindow.xaml
    /// </summary>
    public partial class NewOpdrachtgeverWindow : Window
    {
        private SimulatieService _simService;
        private ObservableCollection<string> _alleopdrachtgevers;
        public NewOpdrachtgeverWindow(SimulatieService simService, ObservableCollection<string> alleopdrachtgevers)
        {
            _simService = simService;
            _alleopdrachtgevers = alleopdrachtgevers;
            InitializeComponent();
        }


        public void SaveClick(object sender, RoutedEventArgs e)
        {
            string nieuwWeOpdrachtGever = NameBox.Text;
            _simService.VoegOpdrachtGeverToe(nieuwWeOpdrachtGever);
            _alleopdrachtgevers.Add(nieuwWeOpdrachtGever);

        }
    }
}