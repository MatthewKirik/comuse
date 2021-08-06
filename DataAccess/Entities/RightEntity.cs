using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class RightEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
    }
}
