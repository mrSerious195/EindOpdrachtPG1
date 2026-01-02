namespace KlantenSimulatorBL.DTO
{
    public class StadDTO
    {
        public StadDTO(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public int Id {  get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
