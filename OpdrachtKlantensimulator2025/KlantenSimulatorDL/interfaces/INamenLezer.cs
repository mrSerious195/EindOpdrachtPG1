using KlantenSimulatorBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDatabaseInitialiserDL.interfaces
{
    public interface INamenLezer
    {
        List<NaamInfo> GeefAlleNamen();
    }
}
