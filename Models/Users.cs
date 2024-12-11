using CinemaTix.Utils;
using System.ComponentModel.DataAnnotations;

namespace CinemaTix.Models
{
    public class Users : BaseProperty
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public EnumUserRole Role { get; set; } = EnumUserRole.User;
    }
}
