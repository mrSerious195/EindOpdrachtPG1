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

        public async Task<List<string>> GeefAlleOpdrachtGeversAsync()
        {
            return await _repo.GeefAlleOpdrachtGevers();
        }

        public async Task VoegOpdrachtGeverToe(string naam)
        {
            await _repo.VoegOpdrachtGeverToe(naam);
        }


    }
}