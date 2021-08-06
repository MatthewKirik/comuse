using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.History
{
    public class HE_TaskCategoryChangedEntity : HistoryEventEntity
    {
        public int? TaskId { get; set; }
        public TaskEntity Task { get; set; }

        public int? OldCategoryId { get; set; }
        public CategoryEntity OldCategory { get; set; }

        public int? NewCategoryId { get; set; }
        public CategoryEntity NewCategory { get; set; }
    }
}
