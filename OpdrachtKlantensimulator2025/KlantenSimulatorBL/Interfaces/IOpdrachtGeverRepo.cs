namespace KlantenSimulatorBL.Interfaces
{
    public interface IOpdrachtGeverRepo
    {
        public Task<List<string>> GeefAlleOpdrachtGevers();
        public Task VoegOpdrachtGeverToe(string naam);
    }
}