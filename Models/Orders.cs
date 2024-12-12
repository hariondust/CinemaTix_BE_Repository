namespace CinemaTix.Models
{
    public class Orders : BaseProperty
    {
        public Guid UserId { get; set; }
        public Guid ShowId { get; set; }
        public DateTime Date { get; set; }

        public virtual Users? Users { get; set; }
        public virtual Shows? Shows { get; set; }
    }
}
