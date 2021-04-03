using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Floxx.Infrastructure.Migrations
{
    public partial class WorkflowPathFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowPath_Step_SuccessorStepId",
                table: "WorkflowPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowPath",
                table: "WorkflowPath");

            migrationBuilder.AlterColumn<Guid>(
                name: "SuccessorStepId",
                table: "WorkflowPath",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "WorkflowPath",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WorkflowPath",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "WorkflowPath",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "WorkflowPath",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "WorkflowPath",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowPath",
                table: "WorkflowPath",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPath_StepId_SuccessorStepId",
                table: "WorkflowPath",
                columns: new[] { "StepId", "SuccessorStepId" },
                unique: true,
                filter: "[SuccessorStepId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowPath_Step_SuccessorStepId",
                table: "WorkflowPath",
                column: "SuccessorStepId",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowPath_Step_SuccessorStepId",
                table: "WorkflowPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowPath",
                table: "WorkflowPath");

            migrationBuilder.DropIndex(
                name: "IX_WorkflowPath_StepId_SuccessorStepId",
                table: "WorkflowPath");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WorkflowPath");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WorkflowPath");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "WorkflowPath");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "WorkflowPath");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "WorkflowPath");

            migrationBuilder.AlterColumn<Guid>(
                name: "SuccessorStepId",
                table: "WorkflowPath",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowPath",
                table: "WorkflowPath",
                columns: new[] { "StepId", "SuccessorStepId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowPath_Step_SuccessorStepId",
                table: "WorkflowPath",
                column: "SuccessorStepId",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
