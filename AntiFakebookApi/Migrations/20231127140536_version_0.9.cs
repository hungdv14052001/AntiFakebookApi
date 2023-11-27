using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiFakebookApi.Migrations
{
    public partial class version_09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuggestedFriends",
                table: "PushSettings",
                newName: "SuggestedFriend");

            migrationBuilder.RenameColumn(
                name: "RequestedFriends",
                table: "PushSettings",
                newName: "RequestedFriend");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 14, 5, 36, 613, DateTimeKind.Utc).AddTicks(2415));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuggestedFriend",
                table: "PushSettings",
                newName: "SuggestedFriends");

            migrationBuilder.RenameColumn(
                name: "RequestedFriend",
                table: "PushSettings",
                newName: "RequestedFriends");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 13, 50, 24, 187, DateTimeKind.Utc).AddTicks(8779));
        }
    }
}
