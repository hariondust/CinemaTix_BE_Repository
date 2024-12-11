namespace CinemaTix.Models
{
    public class Reviews : BaseProperty
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public double Rating { get; set; } // 1 - 5
        public string? Review { get; set; }

        public required virtual Orders Orders { get; set; }
    }
}
