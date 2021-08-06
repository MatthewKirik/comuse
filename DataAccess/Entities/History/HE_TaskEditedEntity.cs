using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_TaskEditedEntity : HistoryEventEntity
    {
        public string OldTitle { get; set; }
        public string NewTitle { get; set; }

        public string OldDescription { get; set; }
        public string NewDescription { get; set; }

        public int? TaskId { get; set; }
        public TaskEntity Task { get; set; }
    }
}
