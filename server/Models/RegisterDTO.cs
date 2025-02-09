namespace server.Models
{
    public class RegisterDTO
    {
        public required string Username { get; set; }
        public required string HashedPassword { get; set; }
    }
}
