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
    /// Interaction logic for LandDatasetInfo.xaml
    /// </summary>
    public partial class LandDatasetInfo : Window
    {

        public LandDatasetInfo(SimulatieService service, int datasetId)
        {
            InitializeComponent();
            initialiseAsync(service, datasetId);
        }


        public async void initialiseAsync(SimulatieService service, int datasetId)
        {
            StedenGrid.ItemsSource=await service.GeefInfoStedenAsync(datasetId);
            var info = await service.GeefSimpeleDataSetInfo(datasetId);
            InfoBox.Text = info.ToString();
        }
    }
}
