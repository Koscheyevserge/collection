using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Api.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResultId",
                table: "Tasks",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskResultsItems_ResultId",
                table: "Tasks",
                column: "ResultId",
                principalTable: "TaskResultsItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskResultsItems_ResultId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ResultId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Tasks");
        }
    }
}
