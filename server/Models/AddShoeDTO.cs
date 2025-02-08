namespace server.Models
{
    public class AddShoeDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int Size { get; set; }
        public required string Brand { get; set; }
        public Guid FactoryId { get; set; }
    }
}
