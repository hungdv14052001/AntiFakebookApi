using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiFakebookApi.Migrations
{
    public partial class version_07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PushSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    LikeComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromFriends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedFriends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestedFriends = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoundOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VibrandOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LetOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushSettings", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 27, 13, 38, 1, 356, DateTimeKind.Utc).AddTicks(9161));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushSettings");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 11, 21, 7, 37, 38, 224, DateTimeKind.Utc).AddTicks(9796));
        }
    }
}
