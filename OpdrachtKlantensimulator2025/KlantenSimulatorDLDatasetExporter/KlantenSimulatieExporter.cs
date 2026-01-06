using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlantenSimulatorBL.Domein;
using KlantenSimulatorBL.Interfaces;

namespace KlantenSimulatorDLDatasetExporter
{
    public class KlantenSimulatieExporter: IKlantenSimulatieExporter
    {
        public void ExportMetaDataAsText(SimpeleSimulatieInfoDTO simulatieInfoDTO, Simulatie simulatie);

        public void ExportMetaDataEnKlantenAsText(SimpeleSimulatieInfoDTO simulatieInfoDTO, Simulatie simulatie);

        public void ExportMetaDataAsJson(SimpeleSimulatieInfoDTO simulatieInfoDTO, Simulatie simulatie);
    }
}
