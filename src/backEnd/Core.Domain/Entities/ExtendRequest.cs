using Core.Domain.Entities.Base;
using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class ExtendRequest : BaseEntity
    {

        [StringLength(9)]
        public required string StudentCode { get; set; }
        public required Article Article { get; set; }
        public required RequestStatus Status { get; set; }
        public required string EvidenceFileUrl { get; set; }

        [ForeignKey(nameof(StudentCode))]
        public virtual Student? Student { get; set; }

        /// <summary>
        /// The reason associated with the current operation or state, maximum length of 420 characters.
        /// </summary>
        [StringLength(420)]
        public required string Justification { get; set; }
    }
}
