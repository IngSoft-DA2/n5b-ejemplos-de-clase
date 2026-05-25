namespace Cine.Contracts;

public sealed record ActorDto
{
    public required string Nombre { get; init; }
    public required string Apellido { get; init; }
    public double Calificacion { get; init; }
    public required string Foto { get; init; }
}