using DataAccess.Entities;
using DataAccess.Entities.History;
using DataTransfer.Models.History;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ComuseContext : DbContext
    {
        public ComuseContext(DbContextOptions<ComuseContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CategoryEntity>()
                .HasOne(x => x.Space)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.SpaceId);
            modelBuilder
                .Entity<CategoryEntity>()
                .HasOne(x => x.LastHistoryEvent)
                .WithMany(x => x.LastInCategories)
                .HasForeignKey(x => x.LastHistoryEventId);
            modelBuilder
                .Entity<CategoryEntity>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder
                .Entity<RightEntity>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Rights);

            modelBuilder
                .Entity<RoleEntity>()
                .HasOne(x => x.Space)
                .WithMany(x => x.Roles)
                .HasForeignKey(x => x.SpaceId);
            modelBuilder
                .Entity<RoleEntity>()
                .HasMany(x => x.AppliedToMemberships)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

            modelBuilder
                .Entity<SpaceEntity>()
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatedSpaces)
                .HasForeignKey(x => x.CreatorId);
            modelBuilder
                .Entity<SpaceEntity>()
                .HasMany(x => x.TaskStatuses)
                .WithOne(x => x.Space)
                .HasForeignKey(x => x.SpaceId);
            modelBuilder
                .Entity<SpaceEntity>()
                .HasMany(x => x.Members)
                .WithOne(x => x.Space)
                .HasForeignKey(x => x.SpaceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<SpaceMembershipEntity>()
                .HasOne(x => x.User)
                .WithMany(x => x.Memberships)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<TaskEntity>()
                .HasOne(x => x.Status)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.StatusId);
            modelBuilder
                .Entity<TaskEntity>()
                .HasOne(x => x.LastHistoryEvent)
                .WithMany(x => x.LastInTasks)
                .HasForeignKey(x => x.LastHistoryEventId);
            modelBuilder
                .Entity<TaskEntity>()
                .HasOne(x => x.ParentTask)
                .WithMany(x => x.ChildrenTasks)
                .HasForeignKey(x => x.ParentTaskId)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder
                .Entity<TaskStatusEntity>()
                .HasOne(x => x.LastHistoryEvent)
                .WithMany(x => x.LastInTaskStatuses)
                .HasForeignKey(x => x.LastHistoryEventId);

            modelBuilder
                .Entity<HistoryEventEntity>()
                .HasOne(x => x.Space)
                .WithMany(x => x.HistoryEvents)
                .HasForeignKey(x => x.SpaceId);
            modelBuilder
                .Entity<HistoryEventEntity>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.CausedEvents)
                .HasForeignKey(x => x.ActorId);
            //modelBuilder
            //    .Entity<HistoryEventEntity>()
            //    .HasDiscriminator<int>("EventType")
            //    .HasValue<HistoryEventEntity>((int)HistoryEventTypes.GENERAL)
            //    .HasValue<HE_CategoryCreatedEntity>((int)HistoryEventTypes.CATEGORY_CREATED)
            //    .HasValue<HE_CategoryDeletedEntity>((int)HistoryEventTypes.CATEGORY_DELETED)
            //    .HasValue<HE_CategoryEditedEntity>((int)HistoryEventTypes.CATEGORY_EDITED)
            //    .HasValue<HE_RoleAssignedEntity>((int)HistoryEventTypes.ROLE_ASSIGNED)
            //    .HasValue<HE_RoleCreatedEntity>((int)HistoryEventTypes.ROLE_CREATED)
            //    .HasValue<HE_RoleEditedEntity>((int)HistoryEventTypes.ROLE_EDITED)
            //    .HasValue<HE_SpaceCreatedEntity>((int)HistoryEventTypes.SPACE_CREATED)
            //    .HasValue<HE_SpaceEditedEntity>((int)HistoryEventTypes.SPACE_EDITED)
            //    .HasValue<HE_SpaceImageChanged>((int)HistoryEventTypes.SPACE_IMAGE_CHANGED)
            //    .HasValue<HE_TaskCategoryChangedEntity>((int)HistoryEventTypes.TASK_CATEGORY_CHANGED)
            //    .HasValue<HE_TaskCreatedEntity>((int)HistoryEventTypes.TASK_CREATED)
            //    .HasValue<HE_TaskDeletedEntity>((int)HistoryEventTypes.TASK_DELETED)
            //    .HasValue<HE_TaskEditedEntity>((int)HistoryEventTypes.TASK_EDITED)
            //    .HasValue<HE_TaskStatusChangedEntity>((int)HistoryEventTypes.TASK_STATUS_CHANGED)
            //    .HasValue<HE_TaskStatusCreatedEntity>((int)HistoryEventTypes.TASK_STATUS_CREATED)
            //    .HasValue<HE_TaskStatusDeletedEntity>((int)HistoryEventTypes.TASK_STATUS_DELETED)
            //    .HasValue<HE_TaskStatusEditedEntity>((int)HistoryEventTypes.TASK_STATUS_EDITED)
            //    .HasValue<HE_UserJoinedEntity>((int)HistoryEventTypes.USER_JOINED)
            //    .HasValue<HE_UserLeftEntity>((int)HistoryEventTypes.USER_LEFT);

            modelBuilder
                .Entity<HistoryEventEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.GENERAL));
            modelBuilder
                .Entity<HE_CategoryCreatedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.CATEGORY_CREATED));
            modelBuilder
                .Entity<HE_CategoryDeletedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.CATEGORY_DELETED));
            modelBuilder
                .Entity<HE_CategoryEditedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.CATEGORY_EDITED));
            modelBuilder
                .Entity<HE_RoleAssignedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.ROLE_ASSIGNED));
            modelBuilder
                .Entity<HE_RoleCreatedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.ROLE_CREATED));
            modelBuilder
                .Entity<HE_RoleEditedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.ROLE_EDITED));
            modelBuilder
                .Entity<HE_SpaceCreatedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.SPACE_CREATED));
            modelBuilder
                .Entity<HE_SpaceEditedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.SPACE_EDITED));
            modelBuilder
                .Entity<HE_SpaceImageChanged>()
                .ToTable(GetHETableName(HistoryEventTypes.SPACE_IMAGE_CHANGED));
            modelBuilder
                .Entity<HE_TaskCategoryChangedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_CATEGORY_CHANGED));
            modelBuilder
                .Entity<HE_TaskCreatedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_CREATED));
            modelBuilder
                .Entity<HE_TaskDeletedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_DELETED));
            modelBuilder
                .Entity<HE_TaskEditedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_EDITED));
            modelBuilder
                .Entity<HE_TaskStatusChangedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_STATUS_CHANGED));
            modelBuilder
                .Entity<HE_TaskStatusCreatedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_STATUS_CREATED));
            modelBuilder
                .Entity<HE_TaskStatusDeletedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_STATUS_DELETED));
            modelBuilder
                .Entity<HE_TaskStatusEditedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.TASK_STATUS_EDITED));
            modelBuilder
                .Entity<HE_UserJoinedEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.USER_JOINED));
            modelBuilder
                .Entity<HE_UserLeftEntity>()
                .ToTable(GetHETableName(HistoryEventTypes.USER_LEFT)); ;
        }

        private string GetHETableName(HistoryEventTypes type)
            => "HistoryEvents_" + type.ToString().ToLower();

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<RightEntity> Rights { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SpaceEntity> Spaces { get; set; }
        public DbSet<SpaceMembershipEntity> SpacesMemberships { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskStatusEntity> TaskStatuses { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<HistoryEventEntity> HistoryEvents { get; set; }
        #region HistoryEvents
        public DbSet<HE_CategoryCreatedEntity> HE_CategoryCreatedEntities { get; set; }
        public DbSet<HE_CategoryDeletedEntity> HE_CategoryDeletedEntities { get; set; }
        public DbSet<HE_CategoryEditedEntity> HE_CategoryEditedEntities { get; set; }
        public DbSet<HE_RoleAssignedEntity> HE_RoleAssignedEntities { get; set; }
        public DbSet<HE_RoleCreatedEntity> HE_RoleCreatedEntities { get; set; }
        public DbSet<HE_RoleEditedEntity> HE_RoleEditedEntities { get; set; }
        public DbSet<HE_SpaceCreatedEntity> HE_SpaceCreatedEntities { get; set; }
        public DbSet<HE_SpaceEditedEntity> HE_SpaceEditedEntities { get; set; }
        public DbSet<HE_SpaceImageChanged> HE_SpaceImageChangeds { get; set; }
        public DbSet<HE_TaskCategoryChangedEntity> HE_TaskCategoryChangedEntities { get; set; }
        public DbSet<HE_TaskCreatedEntity> HE_TaskCreatedEntities { get; set; }
        public DbSet<HE_TaskDeletedEntity> HE_TaskDeletedEntities { get; set; }
        public DbSet<HE_TaskEditedEntity> HE_TaskEditedEntities { get; set; }
        public DbSet<HE_TaskStatusChangedEntity> HE_TaskStatusChangedEntities { get; set; }
        public DbSet<HE_TaskStatusCreatedEntity> HE_TaskStatusCreatedEntities { get; set; }
        public DbSet<HE_TaskStatusDeletedEntity> HE_TaskStatusDeletedEntities { get; set; }
        public DbSet<HE_TaskStatusEditedEntity> HE_TaskStatusEditedEntities { get; set; }
        public DbSet<HE_UserJoinedEntity> HE_UserJoinedEntities { get; set; }
        public DbSet<HE_UserLeftEntity> HE_UserLeftEntities { get; set; }
        #endregion
    }
}

