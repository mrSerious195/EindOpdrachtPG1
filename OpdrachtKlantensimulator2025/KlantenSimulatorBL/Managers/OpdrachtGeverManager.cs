using KlantenSimulatorBL.Interfaces;

namespace KlantenSimulatorBL.Managers
{
    public class OpdrachtGeverManager
    {

        private IOpdrachtGeverRepo _repo;

        public OpdrachtGeverManager(IOpdrachtGeverRepo repo)
        {
            _repo = repo;
        }

        public List<string> GeefAlleOpdrachtGevers()
        {
            return _repo.GeefAlleOpdrachtGevers();
        }

        public void VoegOpdrachtGeverToe(string naam)
        {
            _repo.VoegOpdrachtGeverToe(naam);
        }
    }
}