namespace CinemaTix.Models
{
    public class Orders : BaseProperty
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ShowId { get; set; }
        public DateTime Date { get; set; }

        public required virtual Users Users { get; set; }
        public required virtual Shows Shows { get; set; }
    }
}
