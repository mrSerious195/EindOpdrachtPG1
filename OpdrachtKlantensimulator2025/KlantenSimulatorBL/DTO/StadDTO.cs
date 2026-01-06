namespace KlantenSimulatorBL.DTO
{
    public class StadDTO
    {
        public StadDTO(string name, int id, int aantalStraten)
        {
            Name = name;
            Id = id;
            AantalStraten = aantalStraten;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int AantalStraten { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
