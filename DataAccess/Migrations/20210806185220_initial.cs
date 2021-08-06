using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spaces_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_general",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: true),
                    ActorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_general", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_general_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_general_Users_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    LastHistoryEventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_HistoryEvents_general_LastHistoryEventId",
                        column: x => x.LastHistoryEventId,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_category_deleted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_category_deleted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_category_deleted_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_space_created",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_space_created", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_space_created_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_space_edited",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_space_edited", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_space_edited_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_space_image_changed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OldImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_space_image_changed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_space_image_changed_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_deleted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_deleted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_deleted_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_status_deleted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_status_deleted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_deleted_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_user_joined",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    InvitorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_user_joined", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_user_joined_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_user_joined_Users_InvitorId",
                        column: x => x.InvitorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_user_joined_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_user_left",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_user_left", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_user_left_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_user_left_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    LastHistoryEventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskStatuses_HistoryEvents_general_LastHistoryEventId",
                        column: x => x.LastHistoryEventId,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskStatuses_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_role_assigned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_role_assigned", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_assigned_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_assigned_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_assigned_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_role_created",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_role_created", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_created_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_created_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_role_edited",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_role_edited", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_edited_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_role_edited_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RightEntityRoleEntity",
                columns: table => new
                {
                    RightsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RightEntityRoleEntity", x => new { x.RightsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RightEntityRoleEntity_Rights_RightsId",
                        column: x => x.RightsId,
                        principalTable: "Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RightEntityRoleEntity_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpacesMemberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacesMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpacesMemberships_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpacesMemberships_Spaces_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Spaces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpacesMemberships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_category_created",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_category_created", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_category_created_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_category_created_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_category_edited",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_category_edited", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_category_edited_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_category_edited_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_status_created",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_status_created", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_created_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_created_TaskStatuses_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_status_edited",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TaskStatusId = table.Column<int>(type: "int", nullable: true),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_status_edited", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_edited_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_edited_TaskStatuses_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    LastHistoryEventId = table.Column<int>(type: "int", nullable: true),
                    ParentTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_HistoryEvents_general_LastHistoryEventId",
                        column: x => x.LastHistoryEventId,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Tasks_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_category_changed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    OldCategoryId = table.Column<int>(type: "int", nullable: true),
                    NewCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_category_changed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_category_changed_Categories_NewCategoryId",
                        column: x => x.NewCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_category_changed_Categories_OldCategoryId",
                        column: x => x.OldCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_category_changed_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_category_changed_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_created",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_created", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_created_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_created_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_edited",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OldTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_edited", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_edited_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_edited_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents_task_status_changed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    OldTaskStatusId = table.Column<int>(type: "int", nullable: true),
                    NewTaskStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents_task_status_changed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_changed_HistoryEvents_general_Id",
                        column: x => x.Id,
                        principalTable: "HistoryEvents_general",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_changed_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_changed_TaskStatuses_NewTaskStatusId",
                        column: x => x.NewTaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_task_status_changed_TaskStatuses_OldTaskStatusId",
                        column: x => x.OldTaskStatusId,
                        principalTable: "TaskStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastHistoryEventId",
                table: "Categories",
                column: "LastHistoryEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SpaceId",
                table: "Categories",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_category_created_CategoryId",
                table: "HistoryEvents_category_created",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_category_edited_CategoryId",
                table: "HistoryEvents_category_edited",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_general_ActorId",
                table: "HistoryEvents_general",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_general_SpaceId",
                table: "HistoryEvents_general",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_role_assigned_RoleId",
                table: "HistoryEvents_role_assigned",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_role_assigned_UserId",
                table: "HistoryEvents_role_assigned",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_role_created_RoleId",
                table: "HistoryEvents_role_created",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_role_edited_RoleId",
                table: "HistoryEvents_role_edited",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_category_changed_NewCategoryId",
                table: "HistoryEvents_task_category_changed",
                column: "NewCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_category_changed_OldCategoryId",
                table: "HistoryEvents_task_category_changed",
                column: "OldCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_category_changed_TaskId",
                table: "HistoryEvents_task_category_changed",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_created_TaskId",
                table: "HistoryEvents_task_created",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_edited_TaskId",
                table: "HistoryEvents_task_edited",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_status_changed_NewTaskStatusId",
                table: "HistoryEvents_task_status_changed",
                column: "NewTaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_status_changed_OldTaskStatusId",
                table: "HistoryEvents_task_status_changed",
                column: "OldTaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_status_changed_TaskId",
                table: "HistoryEvents_task_status_changed",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_status_created_TaskStatusId",
                table: "HistoryEvents_task_status_created",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_task_status_edited_TaskStatusId",
                table: "HistoryEvents_task_status_edited",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_user_joined_InvitorId",
                table: "HistoryEvents_user_joined",
                column: "InvitorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_user_joined_UserId",
                table: "HistoryEvents_user_joined",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_user_left_UserId",
                table: "HistoryEvents_user_left",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RightEntityRoleEntity_RolesId",
                table: "RightEntityRoleEntity",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_SpaceId",
                table: "Roles",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Spaces_CreatorId",
                table: "Spaces",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SpacesMemberships_RoleId",
                table: "SpacesMemberships",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SpacesMemberships_SpaceId",
                table: "SpacesMemberships",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SpacesMemberships_UserId",
                table: "SpacesMemberships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LastHistoryEventId",
                table: "Tasks",
                column: "LastHistoryEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ParentTaskId",
                table: "Tasks",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatuses_LastHistoryEventId",
                table: "TaskStatuses",
                column: "LastHistoryEventId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatuses_SpaceId",
                table: "TaskStatuses",
                column: "SpaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEvents_category_created");

            migrationBuilder.DropTable(
                name: "HistoryEvents_category_deleted");

            migrationBuilder.DropTable(
                name: "HistoryEvents_category_edited");

            migrationBuilder.DropTable(
                name: "HistoryEvents_role_assigned");

            migrationBuilder.DropTable(
                name: "HistoryEvents_role_created");

            migrationBuilder.DropTable(
                name: "HistoryEvents_role_edited");

            migrationBuilder.DropTable(
                name: "HistoryEvents_space_created");

            migrationBuilder.DropTable(
                name: "HistoryEvents_space_edited");

            migrationBuilder.DropTable(
                name: "HistoryEvents_space_image_changed");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_category_changed");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_created");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_deleted");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_edited");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_status_changed");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_status_created");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_status_deleted");

            migrationBuilder.DropTable(
                name: "HistoryEvents_task_status_edited");

            migrationBuilder.DropTable(
                name: "HistoryEvents_user_joined");

            migrationBuilder.DropTable(
                name: "HistoryEvents_user_left");

            migrationBuilder.DropTable(
                name: "RightEntityRoleEntity");

            migrationBuilder.DropTable(
                name: "SpacesMemberships");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Rights");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TaskStatuses");

            migrationBuilder.DropTable(
                name: "HistoryEvents_general");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
