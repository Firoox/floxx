using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Floxx.Infrastructure.Migrations
{
    public partial class Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows");

            migrationBuilder.RenameTable(
                name: "Workflows",
                newName: "Workflow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayNameGerman = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayNameEnglish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StatusId1 = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    WorkflowId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Status_StatusId1",
                        column: x => x.StatusId1,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Step_Workflow_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowPath",
                columns: table => new
                {
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuccessorStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayNameGerman = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayNameEnglish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowPath", x => new { x.StepId, x.SuccessorStepId });
                    table.ForeignKey(
                        name: "FK_WorkflowPath_Step_StepId",
                        column: x => x.StepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPath_Step_SuccessorStepId",
                        column: x => x.SuccessorStepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Step_StatusId1",
                table: "Step",
                column: "StatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Step_WorkflowId",
                table: "Step",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPath_SuccessorStepId",
                table: "WorkflowPath",
                column: "SuccessorStepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowPath");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow");

            migrationBuilder.RenameTable(
                name: "Workflow",
                newName: "Workflows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows",
                column: "Id");
        }
    }
}
