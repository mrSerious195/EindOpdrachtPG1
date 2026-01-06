using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.DTO;
using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KlantenSimulatorDLExporters
{
   
    public partial class KlantenSimulatieExporter: IKlantenSimulatieExporter
    {
        private string _saveFolder = @"C:\Users\rowal\Desktop\SaveFolder";


        public void ExportMetaDataAsText(DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant)
        {

            DateTime now = DateTime.Now;
            String opdrachtgever = simdata.OpdrachtGever;
            PrepFolder(now, opdrachtgever);

            MakeMetadataTextFile(now, opdrachtgever, simdata, KlantenInsteden, voornamenF, voornamenM,achternamen, jongsteKlant, oudsteKlant);
            
        }

        private void MakeMetadataTextFile(DateTime now, string opdrachtgever, DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden,  List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant)
        {
            string currentPath = _saveFolder + $"\\{opdrachtgever}\\{now.Ticks}\\Metadata.txt";
            if (!File.Exists(currentPath))
            {
                File.Create(currentPath).Dispose();
            }
            using (StreamWriter swMetaData = new(currentPath))
            {
                string mogelijkeLetters = simdata.AantalMogelijkeLettersInHuisNummer == 1 ? "[A]" : $"[A - {(char)('A' - 1 + simdata.AantalMogelijkeLettersInHuisNummer)}]";
                string stadlimiet = simdata.GelimiteerdTotSteden.Count() == 0 ? "Alle steden in de dataset worden gebruikt"
                    : $"De simulatie is gelimiteerd tot de volgende steden {String.Join(", ", simdata.GelimiteerdTotSteden)}";
                swMetaData.WriteLine($"GENERATIE SETTINGS");
                swMetaData.WriteLine($"Opdrachtgever: {simdata.OpdrachtGever}");
                swMetaData.WriteLine($"Dataset: {simdata.NaamDataSet}     Land: {simdata.Land}");
                swMetaData.WriteLine($"Aantal klanten: {simdata.SimulatieGrote}");
                swMetaData.WriteLine($"Minimum leeftijd: {simdata.MinimumleeftijdSettings}       Maximum leeftijd: {simdata.MaximumleeftijdSettings}");
                swMetaData.WriteLine($"Kans op een letter in het huisnummer: {simdata.KansOpLettersInHuisNummer}       Mogelijke Letters: {mogelijkeLetters}");
                swMetaData.WriteLine(stadlimiet);
                swMetaData.WriteLine($"Opdrachtgever: {simdata.OpdrachtGever}");
                swMetaData.WriteLine($"Opdrachtgever: {simdata.OpdrachtGever}");
                swMetaData.WriteLine($"Opdrachtgever: {simdata.OpdrachtGever}");
                swMetaData.WriteLine("");
                swMetaData.WriteLine($"EXTRA INFO");
                swMetaData.WriteLine($"Gemidelde leeftijd Op aanmaak {simdata.GemideldeSimulatieLeefrijdOpMomentVanAanMaak} jaar");
                swMetaData.WriteLine($"Gemidelde leeftijd Op nu {simdata.GemideldeSimulatieLeefrijdOpMomentVanOpvraagGegenens} jaar");
                swMetaData.WriteLine($"De jongste klant is: {jongsteKlant.Voornaam} {jongsteKlant.AchterNaam} geboren op {jongsteKlant.GeboorteDatum.ToLongDateString()} en wondende {jongsteKlant.Straat} {jongsteKlant.HuisNr}, {jongsteKlant.Stad} ({jongsteKlant.Land})");
                swMetaData.WriteLine($"De Oudste klant is: {oudsteKlant.Voornaam} {oudsteKlant.AchterNaam} geboren op {oudsteKlant.GeboorteDatum.ToLongDateString()} en wondende {oudsteKlant.Straat} {oudsteKlant.HuisNr}, {oudsteKlant.Stad} ({oudsteKlant.Land})");


                swMetaData.WriteLine("");
                swMetaData.WriteLine($"KLANTENDISTRIBUTIE PER STAD");
                foreach (StadMetAantalKlantenDTO sk in KlantenInsteden)
                {
                    swMetaData.WriteLine($"{sk.Stad}:{sk.aantalKlanten}");
                }
                swMetaData.WriteLine("");
                swMetaData.WriteLine($"VROUWEN MET VOORNAMEN");
                foreach (NaamMetAantalKlantedDTO nk in voornamenF)
                {
                    swMetaData.WriteLine($"{nk.Naam}:{nk.AantalKlantenMetNaam}");
                }
                swMetaData.WriteLine("");
                swMetaData.WriteLine($"MANNEN MET VOORNAMEN");
                foreach (NaamMetAantalKlantedDTO nk in voornamenM)
                {
                    swMetaData.WriteLine($"{nk.Naam}:{nk.AantalKlantenMetNaam}");
                }
                swMetaData.WriteLine("");
                swMetaData.WriteLine($"MENSEN MET ACHTERNAMEN");
                foreach (NaamMetAantalKlantedDTO nk in achternamen)
                {
                    swMetaData.WriteLine($"{nk.Naam}:{nk.AantalKlantenMetNaam}");
                }
            }
        }
        public void ExportMetaDataEnKlantenAsText(char sperator, List<Klant> klanten, DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant)
        {
            DateTime now = DateTime.Now;
            String opdrachtgever = simdata.OpdrachtGever;
            PrepFolder(now, opdrachtgever);

            MakeMetadataTextFile(now, opdrachtgever, simdata, KlantenInsteden, voornamenF,voornamenM,achternamen, jongsteKlant, oudsteKlant);
            string KlantenPad = _saveFolder + $"\\{opdrachtgever}\\{now.Ticks}\\Klanten.txt";
            using (StreamWriter swMetaData = new(KlantenPad))
            {
                foreach (Klant k in klanten)
                {
                    List<string> data = [k.Id.ToString(),k.VoorNaam, k.Familienaam,k.GeboorteDatum.ToString(),k.Geslacht.ToString()
                        ,k.Adress.HuisNr,
                        k.Adress.Straat.Id.ToString(),k.Adress.Straat.Naam,
                        k.Adress.Straat.Stad.Id.ToString(),k.Adress.Straat.Stad.Naam];
                    swMetaData.WriteLine(String.Join(sperator, data));
                }
            }
        }

        public void ExportMetaDataAsJson(DetailedSimulatieDTO simdata, List<StadMetAantalKlantenDTO> KlantenInsteden, List<NaamMetAantalKlantedDTO> voornamenF, List<NaamMetAantalKlantedDTO> voornamenM, List<NaamMetAantalKlantedDTO> achternamen, KlantDTO jongsteKlant, KlantDTO oudsteKlant)
        {


            DateTime now = DateTime.Now;
            String opdrachtgever = simdata.OpdrachtGever;

            PrepFolder(now, opdrachtgever);
            string currentPath = _saveFolder + $"\\{opdrachtgever}\\{now.Ticks}\\Metadata.Json";
            MetaDataForJson metadata = new MetaDataForJson(simdata, KlantenInsteden, voornamenF, voornamenM, achternamen, jongsteKlant, oudsteKlant);
            string jsonString = JsonSerializer.Serialize(metadata);
            using (StreamWriter swMetaData = new(currentPath))
            {
                swMetaData.WriteLine(jsonString);
            }
        }

        private void PrepFolder(DateTime now,string opdrachtgever)
        {
            string currentPath = _saveFolder + $"\\{opdrachtgever}";
            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }
            currentPath = currentPath + $"\\{now.Ticks}";
            if (!Directory.Exists(currentPath))
            {
                Directory.CreateDirectory(currentPath);
            }
        }
        
    }
}
