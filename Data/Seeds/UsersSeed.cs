using CinemaTix.Models;
using CinemaTix.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CinemaTix.Data.Seeds
{
    public class UsersSeed
    {
        public static async Task SeedUsers(AppDbContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var users = new List<Users>() {
                    new Users {
                        Id = Constants.AdminUserId,
                        Name = "Nurul",
                        Username = "admin",
                        Password = HashPassword("P@ssw0rdAdmin"),
                        Role = EnumUserRole.Admin
                    },
                    new Users {
                        Name = "Fikri",
                        Username = "fikri",
                        Password = HashPassword("P@ssw0rdUser"),
                        Role = EnumUserRole.User
                    }
                };

            foreach (var user in users)
            {
                user.ProcessData(EnumStatusRecord.Insert, Constants.AdminUserId);
            }

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }

        private static string HashPassword (string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash).ToUpper();
            }
        }
    }
}
