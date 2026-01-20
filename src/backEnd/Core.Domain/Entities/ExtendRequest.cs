using Core.Domain.Entities.Base;
using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class ExtendRequest : BaseEntity
    {
        /// <summary>
        /// Maybe add an email validation attribute later
        /// </summary>
        public required string RequesterEmail { get; set; }
        [StringLength(9)]
        public required string StudentCode { get; set; } 
        public required Articles Article { get; set; }
        public required RequestStatus Status { get; set; }
        public required string DriveViewUrl { get; set; }
        public required string DriveFileId { get; set; }
        /// <summary>
        /// The reason associated with the current operation or state, maximum length of 420 characters.
        /// </summary>
        [StringLength(420)]
        public required string Justification { get; set; }
    }
}
