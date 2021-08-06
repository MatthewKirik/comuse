using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        public string Image { get; set; }

        public List<SpaceEntity> CreatedSpaces { get; set; } = new List<SpaceEntity>();

        public List<SpaceMembershipEntity> Memberships { get; set; } = new List<SpaceMembershipEntity>();

        public List<History.HistoryEventEntity> CausedEvents { get; set; } = new List<History.HistoryEventEntity>();
    }
}
