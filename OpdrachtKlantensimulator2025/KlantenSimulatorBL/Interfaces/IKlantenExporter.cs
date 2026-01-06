using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IKlantenSimulatieExporter
    {
        public void ExportMetaDataAsText(DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant);


        public void ExportMetaDataEnKlantenAsText(char sperator, List<Klant> klanten, DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant);

        public void ExportMetaDataAsJson(DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant);
    }
}
