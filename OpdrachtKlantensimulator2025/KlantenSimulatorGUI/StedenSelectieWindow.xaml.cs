using KlantenSimulatorBL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for StedenSelectieWindow.xaml
    /// </summary>
    public partial class StedenSelectieWindow : Window
    {

        private ObservableCollection<StadDTO> _nietGeselecteerdeSteden;
        private ObservableCollection<StadDTO> _geselecteerdeSteden;
        public StedenSelectieWindow(ObservableCollection<StadDTO> geselecteerdeSteden, ObservableCollection<StadDTO> nietGeselecteerdeSteden)
        {
            InitializeComponent();
            _nietGeselecteerdeSteden= nietGeselecteerdeSteden;
            _geselecteerdeSteden = geselecteerdeSteden;
      

            CollectionViewSource selectiewiew = new CollectionViewSource();
            selectiewiew.Source= _geselecteerdeSteden;
            selectiewiew.SortDescriptions.Add(
                new SortDescription(nameof(StadDTO.Name), ListSortDirection.Ascending));

            CollectionViewSource nietSelectiewiew = new CollectionViewSource();
            nietSelectiewiew.Source = _nietGeselecteerdeSteden;
            nietSelectiewiew.SortDescriptions.Add(
                new SortDescription(nameof(StadDTO.Name), ListSortDirection.Ascending));

            SelectedBox.ItemsSource = selectiewiew.View;
            NonSelectedBox.ItemsSource = nietSelectiewiew.View;
        }

        private void SelectAllClicked(object sender, RoutedEventArgs e)
        {
            foreach (var facil in _nietGeselecteerdeSteden)
            {
                _geselecteerdeSteden.Add(facil);
            }
            _nietGeselecteerdeSteden.Clear();






        }

        private void UnSelectAllClicked(object sender, RoutedEventArgs e)
        {
            foreach (var facil in _geselecteerdeSteden)
            {
                _nietGeselecteerdeSteden.Add(facil);
            }
            _geselecteerdeSteden.Clear();


        }

        private void UnSelectSelected(object sender, RoutedEventArgs e)
        {
            StadDTO selected = null;
            foreach (var s in SelectedBox.SelectedItems)
            {
                if (s is StadDTO select)
                {
                    selected = select;
                    break;
                }
            }
            if (selected != null)
            {
                _nietGeselecteerdeSteden.Add(selected);
                _geselecteerdeSteden.Remove(selected);


            }

        }
        private void SelectSelected(object sender, RoutedEventArgs e)
        {
            StadDTO selected = null;
            foreach (var s in NonSelectedBox.SelectedItems)
            {
                if (s is StadDTO select)
                {
                    selected = select;
                    break;
                }
            }
            if (selected != null)
            {
                _geselecteerdeSteden.Add(selected);
                _nietGeselecteerdeSteden.Remove(selected);


            }
        }

        
    }
}
