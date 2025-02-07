namespace server.Models.Entities
{
    public class Shoe
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int Size { get; set; }
        public required string Brand { get; set; }
        public Guid FactoryId { get; set; }

        public required Factory Factory { get; set; }

        public Shoe()
        {

        }
    }
}
