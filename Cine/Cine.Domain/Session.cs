namespace Cine.Domain;

public sealed class Session
{
    public int Id { get; set; }
    public required string Token { get; set; }
    public int UserId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }

    public User? User { get; set; }
}
