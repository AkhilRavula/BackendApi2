namespace BackendApi2.Entities.Models;
 public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public byte[] PasswordKey { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
