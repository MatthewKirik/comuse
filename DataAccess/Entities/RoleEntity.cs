using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int SpaceId { get; set; }
        [Required]
        public SpaceEntity Space { get; set; }

        public List<RightEntity> Rights { get; set; } = new List<RightEntity>();

        public List<SpaceMembershipEntity> AppliedToMemberships { get; set; } = new List<SpaceMembershipEntity>();
    }
}
