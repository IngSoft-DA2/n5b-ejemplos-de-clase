namespace Cine.Domain;

public sealed class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
