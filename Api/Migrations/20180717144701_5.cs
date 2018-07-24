using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Api.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Accruals");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Accruals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Accruals");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Accruals",
                nullable: false,
                defaultValue: 0);
        }
    }
}
