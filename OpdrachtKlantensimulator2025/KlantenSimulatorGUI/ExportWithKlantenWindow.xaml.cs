using KlantenSimulatorBL.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ExportWithKlantenWindow.xaml
    /// </summary>
    public partial class ExportWithKlantenWindow : Window
    {
        private SimulatieService _simserve;

        private int _simid;
        public ExportWithKlantenWindow(SimulatieService simserve,int simid)
        {
            _simserve = simserve;
            _simid = simid;
            InitializeComponent();
        }

        public async void Export(object sender, RoutedEventArgs e)
        {
            char seperatorchar = charbox.Text[0];
           await _simserve.ExportSimulatieMetaDataAlsTextMetKlanten(_simid, seperatorchar);

            MessageBox.Show("Finished exporting");
            this.Close();

        }
        }
}
