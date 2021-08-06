using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_RoleCreatedEntity : HistoryEventEntity
    {
        public int? RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
