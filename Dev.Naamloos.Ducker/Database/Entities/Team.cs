using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dev.Naamloos.Ducker.Database.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Team
    {
        [Key]
        public ulong Id { get; set; }

        public string Name { get; set; } = default!;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
