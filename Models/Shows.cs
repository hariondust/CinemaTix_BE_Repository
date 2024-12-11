using CinemaTix.Utils;

namespace CinemaTix.Models
{
    public class Shows : BaseProperty
    {
        public Guid Id { get; set; }
        public Guid MoviesId { get; set; }
        public DateTime Schedule { get; set; }
        public int Price { get; set; } // in IDR
        public int TotalSeat { get; set; }
        public EnumShowStatus Status { get; set; } = EnumShowStatus.Available;

        public virtual required Movies Movies { get; set; }
    }
}
