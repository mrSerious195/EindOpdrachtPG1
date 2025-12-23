namespace KlantenSimulatorBL.DTO
{
    public class StadDTO
    {
        public StadDTO(string name, int? id, string[] straten)
        {
            Name = name;
            Id = id;
            Straten = straten;
        }

        public string Name { get; set; }
        public int? Id {  get; set; }

        string[] Straten { get; set; }
    }
}
