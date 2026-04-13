namespace Cine.Contracts;

public sealed class ApiResponseDto
{
    public bool ExecutionSuccessful { get; set; }
    public required string Message { get; set; }
    public string? ErrorCode { get; set; }
    public string? Details { get; set; }
}
