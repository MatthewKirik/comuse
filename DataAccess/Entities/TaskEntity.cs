using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public int StatusId { get; set; }
        [Required]
        public TaskStatusEntity Status { get; set; }

        public int? CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public int? LastHistoryEventId { get; set; }
        public History.HistoryEventEntity LastHistoryEvent { get; set; }

        public int? ParentTaskId { get; set; }
        public TaskEntity ParentTask { get; set; }
        public List<TaskEntity> ChildrenTasks { get; set; } = new List<TaskEntity>();
    }
}
