using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_UserJoinedEntity : HistoryEventEntity
    {
        public int? UserId { get; set; }
        public UserEntity User { get; set; }

        public int? InvitorId { get; set; }
        public UserEntity Invitor { get; set; }
    }
}
