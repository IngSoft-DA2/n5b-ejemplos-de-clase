namespace Cine.Contracts;

public sealed class LoginRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
