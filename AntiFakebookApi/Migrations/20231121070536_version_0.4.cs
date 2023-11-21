using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiFakebookApi.Migrations
{
    public partial class version_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Coins",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 21, 7, 5, 36, 621, DateTimeKind.Utc).AddTicks(9020));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coins",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 21, 6, 55, 54, 471, DateTimeKind.Utc).AddTicks(2992));
        }
    }
}
