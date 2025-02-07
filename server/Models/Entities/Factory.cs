namespace server.Models.Entities
{
    public class Factory
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }

        public List<Shoe>? Shoes { get; set; }
        public Factory() { }
    }
}
