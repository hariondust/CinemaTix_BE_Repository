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
        public char StatusRecord { get; set; } = Constants.StatusRecordInsert;

        public void ProcessData (char statusRecord, Guid? UsersId = null)
        {
            if (statusRecord == Constants.StatusRecordInsert)
            {
                CreatedDate = DateTime.Now;
                CreatedBy = UsersId ?? Constants.AdminUserId;
                StatusRecord = Constants.StatusRecordInsert;
            } 
            else if (statusRecord == Constants.StatusRecordUpdate)
            {
                UpdatedDate = DateTime.Now;
                UpdatedBy = UsersId ?? Constants.AdminUserId;
                StatusRecord = Constants.StatusRecordUpdate;
            } 
            else if (statusRecord == Constants.StatusRecordDelete)
            {
                DeletedDate = DateTime.Now;
                DeletedBy = UsersId ?? Constants.AdminUserId;
                StatusRecord = Constants.StatusRecordDelete;
            }
            else
            {
                throw new ArgumentException("Invalid Status Record");
            }
        }
    }
}
