using CinemaTix.Data;
using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;

namespace CinemaTix.Data.Seeds
{
    public class MoviesSeed
    {
        public static async Task SeedMovies(AppDbContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            var movies = new List<Movies>()
            {
                    new Movies { Id = Constants.MovieId1, Title = "Venom: The Last Dance", Duration = 109, Rating = 0.0d, Synopsis = "Eddie Brock and Venom must make a devastating decision as they're pursued by a mysterious military man and alien monsters from Venom's home world." },
                    new Movies { Id = Constants.MovieId2, Title = "Avengers: Endgame", Duration = 181, Rating = 0.0d, Synopsis = "The Avengers assemble one final time to undo the devastation caused by Thanos and restore balance to the universe." },
                    new Movies { Id = Constants.MovieId3, Title = "The Dark Knight", Duration = 152, Rating = 0.0d, Synopsis = "Batman faces off against the Joker, a criminal mastermind causing chaos in Gotham City." },
                    new Movies { Id = Constants.MovieId4, Title = "Inception", Duration = 148, Rating = 0.0d, Synopsis = "A skilled thief who steals secrets through dreams must plant an idea into a target's mind to achieve redemption." },
                    new Movies { Id = Constants.MovieId5, Title = "Titanic", Duration = 195, Rating = 0.0d, Synopsis = "A love story unfolds on the ill-fated voyage of the Titanic." },
                    new Movies { Id = Constants.MovieId6, Title = "The Matrix", Duration = 136, Rating = 0.0d, Synopsis = "A hacker discovers the world he lives in is a simulated reality and joins a rebellion against its controllers." },
                    new Movies { Id = Constants.MovieId7, Title = "Jurassic Park", Duration = 127, Rating = 0.0d, Synopsis = "Scientists bring dinosaurs back to life, leading to chaos when the creatures escape." },
                    new Movies { Id = Constants.MovieId8, Title = "The Lion King", Duration = 88, Rating = 0.0d, Synopsis = "A young lion prince flees his kingdom but must return to reclaim his rightful throne." },
                    new Movies { Id = Constants.MovieId9, Title = "Harry Potter and the Sorcerer's Stone", Duration = 152, Rating = 0.0d, Synopsis = "A young boy discovers he's a wizard and attends a magical school where he uncovers his destiny." },
                    new Movies { Id = Constants.MovieId10, Title = "Forrest Gump", Duration = 142, Rating = 0.0d, Synopsis = "A kind-hearted man with a low IQ tells his extraordinary life story." },
                    new Movies { Id = Constants.MovieId11, Title = "Star Wars: A New Hope", Duration = 121, Rating = 0.0d, Synopsis = "A farm boy joins a rebellion to save the galaxy from the Empire's Death Star." }
            };

            foreach (var movie in movies)
            {
                movie.ProcessData(Constants.StatusRecordInsert, Constants.AdminUserId);
            };

            context.Movies.AddRange(movies);
            await context.SaveChangesAsync();
        }
    }
}