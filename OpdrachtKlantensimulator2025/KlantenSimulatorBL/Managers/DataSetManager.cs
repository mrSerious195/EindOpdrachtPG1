using KlantenSimulatorBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Managers
{
    public class DataSetManager
    {
        private IDataSetRepo _repo;

        public DataSetManager(IDataSetRepo repo)
        {
            _repo = repo;
        }
    }
}
