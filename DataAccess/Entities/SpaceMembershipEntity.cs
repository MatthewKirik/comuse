using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class SpaceMembershipEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [Required]
        public UserEntity User { get; set; }

        public int SpaceId { get; set; }
        [Required]
        public SpaceEntity Space { get; set; }

        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
