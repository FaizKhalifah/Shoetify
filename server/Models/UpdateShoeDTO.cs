namespace server.Models
{
    public class UpdateShoeDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int Size { get; set; }
        public required string Brand { get; set; }
        public required Guid FactoryId { get; set; }
    }
}
