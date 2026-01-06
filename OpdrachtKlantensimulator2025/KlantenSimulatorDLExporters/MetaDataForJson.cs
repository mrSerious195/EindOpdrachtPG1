using KlantenSimulatorBL.DTO;

namespace KlantenSimulatorDLExporters
{
   
    public partial class KlantenSimulatieExporter
    {
        public class MetaDataForJson
        {
            public DetailedSimulatieDTO simdata {  get; set; }

            public List<StadMetAantalKlantenDTO> KlantenInsteden { get; set; }

            List<NaamMetAantalKlantedDTO> VoornamenF { get; set; }
            List<NaamMetAantalKlantedDTO> VoornamenM { get; set; }
            List<NaamMetAantalKlantedDTO> Achternamen { get; set; }
            KlantDTO jongsteKlant { get; set; }
            KlantDTO oudsteKlant { get; set; }

            public MetaDataForJson(DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> klantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant)
            {
                this.simdata = simdata;
                KlantenInsteden = klantenInsteden;
                VoornamenF = voornamenF;
                VoornamenM = voornamenM;
                Achternamen = achternamen;
                this.jongsteKlant = jongsteKlant;
                this.oudsteKlant = oudsteKlant;
            }
        }
        
    }
}
