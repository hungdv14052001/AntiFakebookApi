using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiFakebookApi.Migrations
{
    public partial class version_05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Uuid" },
                values: new object[] { new DateTime(2023, 11, 21, 7, 25, 24, 160, DateTimeKind.Utc).AddTicks(6055), "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 21, 7, 5, 36, 621, DateTimeKind.Utc).AddTicks(9020));
        }
    }
}
