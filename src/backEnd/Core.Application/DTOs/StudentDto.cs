namespace Core.Application.DTOs;

public record StudentDto
{
    public int Id { get; init; }
    public required string StudentCode { get; init; }
    public required string FirstName { get; init; }
    public required string SecondName { get; init; }
    public required string Lastname { get; init; }
    public required string SecondLastName { get; init; }
    public required string Email { get; init; }
    public required DateOnly CareerStart { get; init; }
}
