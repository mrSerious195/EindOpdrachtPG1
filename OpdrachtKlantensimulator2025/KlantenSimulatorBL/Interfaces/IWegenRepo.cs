using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlantenSimulatorBL.Domein;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IWegenRepo
    {
        public List<Straat> GeefAlleStraten();
    }
}
