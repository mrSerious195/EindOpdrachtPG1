using KlantenSimulatorBL.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface INamenRepo
    {
        public List<NaamInfo> GeefAlleNamen();
    }
}
