using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TaskStatusEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int SpaceId { get; set; }
        [Required]
        public SpaceEntity Space { get; set; }

        public int? LastHistoryEventId { get; set; }
        public History.HistoryEventEntity LastHistoryEvent { get; set; }

        public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
