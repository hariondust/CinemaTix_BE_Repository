using CinemaTix.Utils;

namespace CinemaTix.Models
{
    public class BaseProperty
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; } = Guid.Empty;
        public DateTime? UpdatedDate { get; set; }
        public Guid DeletedBy { get; set; } = Guid.Empty;
        public DateTime? DeletedDate { get; set; }
        public EnumStatusRecord StatusRecord { get; set; } = EnumStatusRecord.Insert;
    }
}
