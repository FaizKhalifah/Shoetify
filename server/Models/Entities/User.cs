namespace server.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public required string Username { get; set; }
        public required string HashedPassword { get; set; }

        public string Role { get; set; } = "User";
    }
}
