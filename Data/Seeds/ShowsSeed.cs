using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Data.Seeds
{
    public class ShowsSeed
    {
        public static async Task SeedShows(AppDbContext context)
        {
            if (await context.Shows.AnyAsync()) return;

            var shows = new List<Shows>()
            {
                new Shows { MoviesId = Constants.MovieId1, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 01) },
                new Shows { MoviesId = Constants.MovieId2, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 01) },
                new Shows { MoviesId = Constants.MovieId3, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 02) },
                new Shows { MoviesId = Constants.MovieId4, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 02) },
                new Shows { MoviesId = Constants.MovieId5, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 02, 01) },
                new Shows { MoviesId = Constants.MovieId6, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 02, 01) },
                new Shows { MoviesId = Constants.MovieId7, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 02, 01) },
                new Shows { MoviesId = Constants.MovieId8, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 05) },
                new Shows { MoviesId = Constants.MovieId9, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 05) },
                new Shows { MoviesId = Constants.MovieId10, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 05) },
                new Shows { MoviesId = Constants.MovieId11, Price = 40000, TotalSeat = 40, Status = EnumShowStatus.Available, Schedule = new DateTime(2025, 01, 05) }
            };

            foreach (var show in shows)
            {
                show.ProcessData(Constants.StatusRecordInsert, Constants.AdminUserId);
            };

            context.Shows.AddRange(shows);
            await context.SaveChangesAsync();
        }

    }
}
