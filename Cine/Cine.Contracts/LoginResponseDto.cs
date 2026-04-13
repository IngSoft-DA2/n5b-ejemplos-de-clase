namespace Cine.Contracts;

public sealed class LoginResponseDto
{
    public bool ExecutionSuccessful { get; set; }
    public required string Message { get; set; }
    public required string SessionId { get; set; }
    public required string Username { get; set; }
    public DateTime ExpirationDate { get; set; }
}
