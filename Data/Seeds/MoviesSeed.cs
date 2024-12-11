using CinemaTix.Data;
using CinemaTix.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Data.Seeds
{
    public class MoviesSeed
    {
        public static async Task SeedMovies(AppDbContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            context.Movies.AddRange(
                    new Movies { Title = "Venom: The Last Dance", Duration = 109, Rating = 0.0d, Synopsis = "Eddie Brock and Venom must make a devastating decision as they're pursued by a mysterious military man and alien monsters from Venom's home world." }
                );

            await context.SaveChangesAsync();
        }
    }
}