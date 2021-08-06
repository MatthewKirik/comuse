using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_SpaceImageChanged : HistoryEventEntity
    {
        public string OldImage { get; set; }
        public string NewImage { get; set; }
    }
}
