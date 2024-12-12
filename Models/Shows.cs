using CinemaTix.Utils;

namespace CinemaTix.Models
{
    public class Shows : BaseProperty
    {
        public Guid MoviesId { get; set; }
        public DateTime Schedule { get; set; }
        public int Price { get; set; } // in IDR
        public int TotalSeat { get; set; }
        public EnumShowStatus Status { get; set; } = EnumShowStatus.Available;

        public virtual Movies? Movies { get; set; }
    }
}
