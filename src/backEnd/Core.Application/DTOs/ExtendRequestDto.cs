namespace Core.Application.DTOs
{
    public record ExtendRequestDto
    {
        public required string StudentCode { get; init; }
        public required int Article { get; init; }
        public required string EvidenceFileUrl { get; init; }
        public required string Reason { get; init; }
        public string? Status { get; init; }
        public DateTime? CreatedAt { get; init; }
    }
}
