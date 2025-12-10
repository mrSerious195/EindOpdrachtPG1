namespace KlantenSimulatorBL.DTO
{
    public class StadDTO
    {
        public StadDTO(string name, uint? id, string[] straten)
        {
            Name = name;
            Id = id;
            Straten = straten;
        }

        public string Name { get; set; }
        public uint? Id {  get; set; }

        string[] Straten { get; set; }
    }
}
