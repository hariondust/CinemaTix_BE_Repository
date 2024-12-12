using CinemaTix.Utils;

namespace CinemaTix.Models
{
    public class BaseProperty
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; } = Guid.Empty;
        public DateTime? UpdatedDate { get; set; }
        public Guid DeletedBy { get; set; } = Guid.Empty;
        public DateTime? DeletedDate { get; set; }
        public EnumStatusRecord StatusRecord { get; set; } = EnumStatusRecord.Insert;

        public void ProcessData (EnumStatusRecord statusRecord, Guid? UsersId = null)
        {
            switch (statusRecord)
            {
                case EnumStatusRecord.Insert:
                    CreatedDate = DateTime.Now;
                    CreatedBy = UsersId ?? Constants.AdminUserId;
                    StatusRecord = EnumStatusRecord.Insert;
                    break;

                case EnumStatusRecord.Delete:
                    DeletedDate = DateTime.Now;
                    DeletedBy = UsersId ?? Constants.AdminUserId;
                    StatusRecord = EnumStatusRecord.Delete;
                    break;

                case EnumStatusRecord.Update:
                    UpdatedDate = DateTime.Now;
                    UpdatedBy = UsersId ?? Constants.AdminUserId;
                    StatusRecord = EnumStatusRecord.Update;
                    break;

                default:
                    throw new ArgumentException("Invalid Status Record");
            }
        }
    }
}
