using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_TaskStatusCreatedEntity: HistoryEventEntity
    {
        public int? TaskStatusId { get; set; }
        public TaskStatusEntity TaskStatus { get; set; }
    }
}
