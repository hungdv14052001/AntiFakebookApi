using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiFakebookApi.Migrations
{
    public partial class version_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Comments",
                newName: "PostId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 14, 28, 17, 299, DateTimeKind.Utc).AddTicks(9698));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Comments",
                newName: "BlogId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 7, 14, 21, 29, 304, DateTimeKind.Utc).AddTicks(8092));
        }
    }
}
