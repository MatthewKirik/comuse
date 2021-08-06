using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.History
{
    public class HistoryEventEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int? SpaceId { get; set; }
        public SpaceEntity Space { get; set; }

        public int? ActorId { get; set; }
        public UserEntity Actor { get; set; }

        public List<CategoryEntity> LastInCategories { get; set; } = new List<CategoryEntity>();
        public List<TaskEntity> LastInTasks { get; set; } = new List<TaskEntity>();
        public List<TaskStatusEntity> LastInTaskStatuses { get; set; } = new List<TaskStatusEntity>();
    }
}
