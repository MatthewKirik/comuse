using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_SpaceEditedEntity : HistoryEventEntity
    {
        public string OldName { get; set; }
        public string NewName { get; set; }

        public string OldDescription { get; set; }
        public string NewDescription { get; set; }
    }
}
