namespace KlantenSimulatorBL.Interfaces
{
    public interface IOpdrachtGeverRepo
    {
        public List<string> GeefAlleOpdrachtGevers();
        public void VoegOpdrachtGeverToe(string naam);
    }
}