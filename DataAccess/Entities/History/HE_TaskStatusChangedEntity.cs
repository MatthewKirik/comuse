using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_TaskStatusChangedEntity : HistoryEventEntity
    {
        public int? TaskId { get; set; }
        public TaskEntity Task { get; set; }

        public int? OldTaskStatusId { get; set; }
        public TaskStatusEntity OldTaskStatus { get; set; }

        public int? NewTaskStatusId { get; set; }
        public TaskStatusEntity NewTaskStatus { get; set; }
    }
}
