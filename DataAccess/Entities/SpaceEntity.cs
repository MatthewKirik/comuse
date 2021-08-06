using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class SpaceEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int CreatorId { get; set; }
        [Required]
        public UserEntity Creator { get; set; }

        public List<SpaceMembershipEntity> Members { get; set; } = new List<SpaceMembershipEntity>();

        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();

        public List<TaskStatusEntity> TaskStatuses { get; set; } = new List<TaskStatusEntity>();

        public List<History.HistoryEventEntity> HistoryEvents { get; set; } = new List<History.HistoryEventEntity>();
    }
}
