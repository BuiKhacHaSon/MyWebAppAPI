using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebApp.Migrations
{
    public partial class mywebapp11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Agent",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 22, 10, 10, 50, 550, DateTimeKind.Utc).AddTicks(9630));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agent",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Device",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 3, 20, 10, 49, 31, 60, DateTimeKind.Utc).AddTicks(1604));
        }
    }
}
