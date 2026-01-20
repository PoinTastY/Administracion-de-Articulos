namespace Core.Application.DTOs
{
    public class ExtendRequestDto
    {
        public required string RequesterEmail { get; set; }
        public required string StudentCode { get; set; }
        public required int Article { get; set; }
        public required string DriveViewUrl { get; set; }
        public required string DriveFileId { get; set; }
        public required string Reason { get; set; }
    }
}
